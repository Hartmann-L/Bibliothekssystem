Imports System

Module Program
    Structure Buch
        Dim ISBN As String
        Dim Titel As String
        Dim Autor As String
        Dim IstAusgeliehen As Boolean
    End Structure
    Dim bibliothek As New List(Of Buch)

    ''' <summary>
    ''' Einstiegspunkt des Programms.
    ''' </summary>
    Sub Main()

        BuecherAusDateiLaden()
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
    ''' Legt einen neuen Benutzer an.
    ''' </summary>
    Sub BenutzerAnlegen()
        Console.WriteLine("Funktion BenutzerAnlegen noch nicht implementiert.")
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
        Console.WriteLine("Funktion AlleBenutzerAnzeigen noch nicht implementiert.")
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
    Sub BuecherAusDateiLaden()

        Dim pfad As String = "C:\Users\Lukas HARTmann\Documents\Studium Lukas\Informatik\library_books.csv"

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
End Module
