using System;
using System.IO;
using System.Text;

namespace MyInterpreter.Lexer.DataSource
{
    public class FileSource : ISource, IDisposable
    {
        private readonly Stream sourceStream;
        private readonly StreamReader streamReader;
        public TextPosition Position { get; private set; }
        public FileSource(string filename)
        {
            sourceStream = File.OpenRead(filename);
            streamReader = new StreamReader(sourceStream);
            Position = new TextPosition();
        }
        public char CurrentChar 
        { 
            get  
            {
                return !streamReader.EndOfStream ? (char)streamReader.Peek() : '\0';
            }
        }
        public void Next()
        {
            if(streamReader.EndOfStream)
                return;
            Position.NextCharacter(CurrentChar);
            streamReader.Read();
        }
        public string GetLineFromPosition(TextPosition position)
        {
            long previousPosition = sourceStream.Position;
            sourceStream.Position = Position.SourcePosition - Position.Column + 1;

            byte[] buffer = new byte[64];
            sourceStream.Read(buffer, 0, 64);
            sourceStream.Position = previousPosition;
            
            return Encoding.UTF8.GetString(buffer, 0, buffer.Length).Split('\n')[0]; // whole line or 64 characters
        }

        #region IDisposableImplementation
        private bool disposed = false;
        public void Dispose()
        { 
            Dispose(true);
            GC.SuppressFinalize(this);           
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return; 
            if (disposing)
            {
                streamReader.Close();
                sourceStream.Close();
                streamReader.Dispose();
                sourceStream.Dispose();
            }
            disposed = true;
        }
        #endregion
    }

}