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
                if(sourceStream.EndOfStream)
                    return '\0';
                return (char)sourceStream.Peek();
            }
        }

        public TextPosition Position => throw new System.NotImplementedException();

        public void Next()
        {
            if(sourceStream.EndOfStream)
                return;
            sourceStream.Read();
        }
        #region ImplementationIDisposable
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