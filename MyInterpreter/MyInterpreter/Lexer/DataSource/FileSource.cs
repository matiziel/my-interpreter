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
        public string GetPieceOfText(TextPosition position, int leftShift, int rightShift)
        {
            long previousPosition = sourceStream.Position;
            sourceStream.Position = (Position.SourcePosition - leftShift >= 0) ? Position.SourcePosition - leftShift : 0;

            byte[] buffer = new byte[rightShift + leftShift];
            sourceStream.Read(buffer, 0, rightShift + leftShift);

            sourceStream.Position = previousPosition;

            return Encoding.UTF8.GetString(buffer, 0, buffer.Length); 
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
                sourceStream.Dispose();
                streamReader.Dispose();
            }
            disposed = true;
        }
        #endregion
    }

}