Imports System

Module Program
    Structure Buch
        Dim ISBN As String
        Dim Titel As String
        Dim Autor As String
        Dim IstAusgeliehen As Boolean
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
    ''' Leiht ein Buch anhand der ISBN aus.
    ''' </summary>
    Sub BuchAusleihen()
        Console.WriteLine("Funktion BuchAusleihen noch nicht implementiert.")
        Console.ReadLine()
    End Sub


    ''' <summary>
    ''' Gibt ein Buch anhand der ISBN zurück.
    ''' </summary>
    Sub BuchZurueckgeben()
        Console.WriteLine("Funktion BuchZurueckgeben noch nicht implementiert.")
        Console.ReadLine()
    End Sub


    ''' <summary>
    ''' Zeigt alle ausgeliehenen Bücher eines bestimmten Nutzers an.
    ''' </summary>
    Sub AusgelieheneBuecherAnzeigen()
        Console.WriteLine("Funktion AusgelieheneBuecherAnzeigen noch nicht implementiert.")
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

End Module
