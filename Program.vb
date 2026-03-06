Imports System

Module Program
    Structure Buch
        Dim ISBN As String
        Dim Titel As String
        Dim Autor As String
        Dim IstAusgeliehen As Boolean
        Dim AusgeliehenVon As String   ' Nur interne Speicherung, wird nicht angezeigt
    End Structure
    Dim bibliothek As New List(Of Buch)

    Structure Benutzer
        Dim BenutzerID As String
        Dim Name As String
    End Structure
    Dim nutzerListe As New List(Of Benutzer)


    ''' <summary>
    ''' Einstiegspunkt des Programms.
    ''' </summary>
    Sub Main()

        BuecherAusDateiLaden()
        BenutzerAusDateiLaden()

        BegruessungAnzeigen()
        HauptmenueStarten()

    End Sub


    ''' <summary>
    ''' Gibt die Begrüßungsanzeige des Bibliothekssystems aus.
    ''' </summary>
    Sub BegruessungAnzeigen()

        Console.WriteLine("=========================================")
        Console.WriteLine("     Willkommen im Bibliothekssystem")
        Console.WriteLine("=========================================")
        Console.WriteLine()

    End Sub
    ''' <summary>
    ''' Startet das Hauptmenü des Bibliothekssystems.
    ''' </summary>
    Sub HauptmenueStarten()

        Dim auswahl As String = ""

        Do
            Console.Clear()
            BegruessungAnzeigen()

            Console.WriteLine("1 - Benutzer anlegen")
            Console.WriteLine("2 - Alle Bücher der Bibliothek anzeigen")
            Console.WriteLine("3 - Alle hinterlegten Nutzer anzeigen")
            Console.WriteLine("4 - Buch ausleihen (mit ISBN)")
            Console.WriteLine("5 - Buch zurückgeben (mit ISBN)")
            Console.WriteLine("6 - Ausgeliehene Bücher eines Nutzers anzeigen")
            Console.WriteLine("7 - Programm beenden")
            Console.WriteLine()
            Console.Write("Bitte wählen Sie eine Option: ")

            auswahl = Console.ReadLine()

            Select Case auswahl
                Case "1"
                    BenutzerAnlegen()
                Case "2"
                    AlleBuecherAnzeigen()
                Case "3"
                    AlleBenutzerAnzeigen()
                Case "4"
                    BuchAusleihen()
                Case "5"
                    BuchZurueckgeben()
                Case "6"
                    AusgelieheneBuecherAnzeigen()
                Case "7"
                    Console.WriteLine("Programm wird beendet...")
                    Threading.Thread.Sleep(1500)
                    Exit Do
                Case Else
                    Console.WriteLine("Ungültige Eingabe!")
                    Console.ReadLine()
            End Select

        Loop While auswahl <> "7"

    End Sub


    ''' <summary>
    ''' Legt einen neuen Benutzer anhand von Eingabedaten an.
    ''' </summary>
    Sub BenutzerAnlegen()

        Console.Clear()
        BegruessungAnzeigen()

        If nutzerListe.Count >= 999 Then
            Console.WriteLine("Maximale Anzahl von 999 Benutzern erreicht.")
            Console.ReadLine()
            Exit Sub
        End If

        Console.Write("Bitte geben Sie eine dreistellige Benutzer-Nummer ein (000-999): ")
        Dim nummerInput As String = Console.ReadLine()

        Dim nummer As Integer

        If Not Integer.TryParse(nummerInput, nummer) OrElse nummer < 0 OrElse nummer > 999 Then
            Console.WriteLine("Ungültige Nummer!")
            Console.ReadLine()
            Exit Sub
        End If

        Dim nummerFormatiert As String = nummer.ToString("D3")

        Console.Write("Vorname eingeben: ")
        Dim vorname As String = Console.ReadLine().Trim()

        Console.Write("Nachname eingeben: ")
        Dim nachname As String = Console.ReadLine().Trim()

        If Not IstGueltigerName(vorname) OrElse Not IstGueltigerName(nachname) Then
            Console.WriteLine("Vor- und Nachname dürfen nur Buchstaben enthalten!")
            Console.ReadLine()
            Exit Sub
        End If

        Dim neueID As String = "U" & nummerFormatiert

        For Each nutzer In nutzerListe
            If nutzer.BenutzerID = neueID Then
                Console.WriteLine("Diese Benutzer-ID existiert bereits!")
                Console.ReadLine()
                Exit Sub
            End If
        Next

        Dim neuerBenutzer As New Benutzer
        neuerBenutzer.BenutzerID = neueID
        neuerBenutzer.Name = vorname & " " & nachname

        nutzerListe.Add(neuerBenutzer)

        Console.WriteLine()
        Console.WriteLine("Benutzer erfolgreich angelegt:")
        Console.WriteLine(neuerBenutzer.BenutzerID & " - " & neuerBenutzer.Name)
        Console.ReadLine()

    End Sub


    ''' <summary>
    ''' Zeigt alle Bücher der Bibliothek an.
    ''' </summary>
    Sub AlleBuecherAnzeigen()

        Console.Clear()
        BegruessungAnzeigen()

        If bibliothek.Count = 0 Then
            Console.WriteLine("Keine Bücher vorhanden.")
        Else

            For Each buch In bibliothek

                Dim status As String

                If buch.IstAusgeliehen Then
                    status = "Verliehen"
                Else
                    status = "Verfügbar"
                End If

                Console.WriteLine(buch.ISBN & " - " &
                              buch.Titel & " - " &
                              buch.Autor & " - " &
                              status)

            Next

        End If

        Console.WriteLine()
        Console.WriteLine("Enter drücken um zurückzukehren...")
        Console.ReadLine()

    End Sub


    ''' <summary>
    ''' Zeigt alle hinterlegten Benutzer an.
    ''' </summary>
    Sub AlleBenutzerAnzeigen()

        Console.Clear()
        BegruessungAnzeigen()

        If nutzerListe.Count = 0 Then
            Console.WriteLine("Keine Benutzer vorhanden.")
        Else

            For Each nutzer In nutzerListe
                Console.WriteLine(nutzer.BenutzerID & " - " & nutzer.Name)
            Next

        End If

        Console.WriteLine()
        Console.WriteLine("Enter drücken um zurückzukehren...")
        Console.ReadLine()

    End Sub


    ''' <summary>
    ''' Leiht ein Buch anhand der ISBN an einen Benutzer aus.
    ''' </summary>
    Sub BuchAusleihen()

        Console.Clear()
        BegruessungAnzeigen()

        Console.Write("Bitte Benutzer-ID eingeben (z.B. U123): ")
        Dim benutzerID As String = Console.ReadLine().Trim()

        If Not BenutzerExistiert(benutzerID) Then
            Console.WriteLine("Benutzer existiert nicht!")
            Console.ReadLine()
            Exit Sub
        End If

        Console.Write("Bitte ISBN des Buches eingeben: ")
        Dim isbn As String = Console.ReadLine().Trim()

        Dim index As Integer = FindeBuchIndex(isbn)

        If index = -1 Then
            Console.WriteLine("Buch nicht gefunden!")
            Console.ReadLine()
            Exit Sub
        End If

        ' Prüfen ob Buch bereits verliehen
        If bibliothek(index).IstAusgeliehen Then
            Console.WriteLine("Dieses Buch ist bereits verliehen!")
            Console.ReadLine()
            Exit Sub
        End If

        ' Interne Speicherung
        Dim buch As Buch = bibliothek(index)

        buch.IstAusgeliehen = True
        buch.AusgeliehenVon = benutzerID

        bibliothek(index) = buch

        Console.WriteLine("Buch erfolgreich ausgeliehen.")
        Console.ReadLine()

    End Sub


    ''' <summary>
    ''' Gibt ein ausgeliehenes Buch zurück.
    ''' </summary>
    Sub BuchZurueckgeben()

        Console.Clear()
        BegruessungAnzeigen()

        Console.Write("Bitte Benutzer-ID eingeben (z.B. U123): ")
        Dim benutzerID As String = Console.ReadLine().Trim()

        If Not BenutzerExistiert(benutzerID) Then
            Console.WriteLine("Benutzer existiert nicht!")
            Console.ReadLine()
            Exit Sub
        End If

        Console.Write("Bitte ISBN des Buches eingeben: ")
        Dim isbn As String = Console.ReadLine().Trim()

        Dim index As Integer = FindeBuchIndex(isbn)

        If index = -1 Then
            Console.WriteLine("Buch nicht gefunden!")
            Console.ReadLine()
            Exit Sub
        End If

        ' Prüfen ob Buch überhaupt verliehen ist
        If Not bibliothek(index).IstAusgeliehen Then
            Console.WriteLine("Dieses Buch ist aktuell nicht verliehen!")
            Console.ReadLine()
            Exit Sub
        End If

        ' Prüfen ob dieses Buch vom angegebenen Benutzer ausgeliehen wurde
        If bibliothek(index).AusgeliehenVon <> benutzerID Then
            Console.WriteLine("Dieses Buch wurde nicht von diesem Benutzer ausgeliehen!")
            Console.ReadLine()
            Exit Sub
        End If

        ' Struktur korrekt ändern (wegen Value Type!)
        Dim buch As Buch = bibliothek(index)

        buch.IstAusgeliehen = False
        buch.AusgeliehenVon = ""

        bibliothek(index) = buch

        Console.WriteLine("Buch erfolgreich zurückgegeben.")
        Console.ReadLine()

    End Sub


    ''' <summary>
    ''' Zeigt alle ausgeliehenen Bücher eines Benutzers an.
    ''' </summary>
    Sub AusgelieheneBuecherAnzeigen()

        Console.Clear()
        BegruessungAnzeigen()

        Console.Write("Bitte Benutzer-ID eingeben (z.B. U123): ")
        Dim benutzerID As String = Console.ReadLine().Trim()

        If Not BenutzerExistiert(benutzerID) Then
            Console.WriteLine("Benutzer existiert nicht!")
            Console.ReadLine()
            Exit Sub
        End If

        Dim hatBuecher As Boolean = False

        For Each buch In bibliothek

            If buch.IstAusgeliehen AndAlso buch.AusgeliehenVon = benutzerID Then
                Console.WriteLine(buch.ISBN & " - " &
                              buch.Titel & " - " &
                              buch.Autor)
                hatBuecher = True
            End If

        Next

        If Not hatBuecher Then
            Console.WriteLine("Dieser Benutzer hat aktuell keine Bücher ausgeliehen.")
        End If

        Console.WriteLine()
        Console.WriteLine("Enter drücken um zurückzukehren...")
        Console.ReadLine()

    End Sub

    ''' <summary>
    ''' Liest alle Bücher aus der CSV-Datei und speichert sie in der Bibliotheksliste.
    ''' </summary>
    Sub BuecherAusDateiLaden()

        Dim pfad As String = "library_books.csv"

        If IO.File.Exists(pfad) Then

            Dim zeilen() As String = IO.File.ReadAllLines(pfad)

            For i As Integer = 1 To zeilen.Length - 1

                Dim teile() As String = zeilen(i).Split(","c)

                Dim neuesBuch As New Buch
                neuesBuch.ISBN = teile(0).Trim()
                neuesBuch.Titel = teile(1).Trim()
                neuesBuch.Autor = teile(2).Trim()

                If teile(3).Trim().ToLower() = "borrowed" Then
                    neuesBuch.IstAusgeliehen = True
                Else
                    neuesBuch.IstAusgeliehen = False
                End If

                bibliothek.Add(neuesBuch)

            Next

        Else
            Console.WriteLine("CSV-Datei wurde nicht gefunden!")
            Console.ReadLine()
        End If

    End Sub

    ''' <summary>
    ''' Liest alle Benutzer aus der CSV-Datei ein.
    ''' </summary>
    Sub BenutzerAusDateiLaden()

        Dim pfad As String = "library_users.csv"

        If IO.File.Exists(pfad) Then

            Dim zeilen() As String = IO.File.ReadAllLines(pfad)

            For i As Integer = 1 To zeilen.Length - 1 ' Falls Header existiert

                Dim teile() As String = zeilen(i).Split(","c)

                Dim neuerBenutzer As New Benutzer
                neuerBenutzer.BenutzerID = teile(0).Trim()
                neuerBenutzer.Name = teile(1).Trim()

                nutzerListe.Add(neuerBenutzer)

            Next

        Else
            Console.WriteLine("Benutzer-CSV nicht gefunden!")
            Console.ReadLine()
        End If

    End Sub

    ''' <summary>
    ''' Prüft, ob der übergebene Name ausschließlich aus Buchstaben besteht.
    ''' </summary>
    ''' <param name="text">Der zu überprüfende Name.</param>
    ''' <returns>True wenn der Name nur Buchstaben enthält, sonst False.</returns>
    ''' <remarks>
    ''' Wird zur Validierung von Vor- und Nachnamen bei der Benutzeranlage verwendet.
    ''' </remarks>

    Function IstGueltigerName(text As String) As Boolean

        For Each zeichen As Char In text
            If Not Char.IsLetter(zeichen) Then
                Return False
            End If
        Next

        Return True

    End Function

    ''' <summary>
    ''' Sucht ein Buch anhand der ISBN in der Bibliotheksliste und gibt dessen Index zurück.
    ''' </summary>
    ''' <param name="isbn">Die ISBN des Buches, nach der gesucht werden soll.</param>
    ''' <returns>Der Index des Buches in der Liste oder -1, wenn kein Buch mit dieser ISBN gefunden wurde.</returns>
    ''' <remarks>
    ''' Wird verwendet, um schnell auf ein bestimmtes Buch in der Bibliotheksliste zugreifen zu können.
    ''' </remarks>

    Function FindeBuchIndex(isbn As String) As Integer

        For i As Integer = 0 To bibliothek.Count - 1
            If bibliothek(i).ISBN = isbn Then
                Return i
            End If
        Next

        Return -1

    End Function

    ''' <summary>
    ''' Prüft, ob ein Benutzer mit der angegebenen Benutzer-ID in der Benutzerliste existiert.
    ''' </summary>
    ''' <param name="benutzerID">Die Benutzer-ID des zu überprüfenden Nutzers.</param>
    ''' <returns>True wenn der Benutzer in der Liste vorhanden ist, sonst False.</returns>
    ''' <remarks>
    ''' Diese Funktion wird verwendet, um Eingaben des Nutzers zu validieren, 
    ''' beispielsweise beim Ausleihen oder Zurückgeben von Büchern.
    ''' </remarks>

    Function BenutzerExistiert(benutzerID As String) As Boolean

        For Each nutzer In nutzerListe
            If nutzer.BenutzerID = benutzerID Then
                Return True
            End If
        Next

        Return False

    End Function

End Module
