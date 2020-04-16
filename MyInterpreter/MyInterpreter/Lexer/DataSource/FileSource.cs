using System;
using System.IO;

namespace MyInterpreter.Lexer.DataSource
{
    public class FileSource : ISource, IDisposable
    {
        private readonly StreamReader sourceStream;
        public FileSource(string filename)
        {
            sourceStream = new StreamReader(filename);
        }
        public char CurrentChar 
        { 
            get  
            {
                return !sourceStream.EndOfStream ? (char)sourceStream.Peek() : '\0';
            }
        }

        public TextPosition Position => throw new NotImplementedException();
        public void Next()
        {
            if(sourceStream.EndOfStream)
                return;
            sourceStream.Read();
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
                sourceStream.Dispose();
            disposed = true;
        }
        #endregion
    }

}