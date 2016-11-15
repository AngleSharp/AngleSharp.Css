namespace AngleSharp.Css.Tests.Mocks
{
    using System;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;

    sealed class DelayedStream : Stream
    {
        private Stream _stream;
        private const Int32 delay = 10;

        public DelayedStream(Stream stream)
        {
            _stream = stream;
        }

        public DelayedStream(Byte[] content)
            : this(new MemoryStream(content))
        {
        }

        public override Boolean CanRead
        {
            get { return _stream.CanRead; }
        }

        public override Boolean CanSeek
        {
            get { return _stream.CanSeek; }
        }

        public override Boolean CanWrite
        {
            get { return _stream.CanWrite; }
        }

        public override void Flush()
        {
            _stream.Flush();
        }

        public override Int64 Length
        {
            get { return _stream.Length; }
        }

        public override Int64 Position
        {
            get { return _stream.Position; }
            set { _stream.Position = value; }
        }

        public override async Task CopyToAsync(Stream destination, Int32 bufferSize, CancellationToken cancellationToken)
        {
            await Task.Delay(delay).ConfigureAwait(false);
            await _stream.CopyToAsync(destination, bufferSize, cancellationToken).ConfigureAwait(false);
        }

        public override async Task<Int32> ReadAsync(Byte[] buffer, Int32 offset, Int32 count, CancellationToken cancellationToken)
        {
            await Task.Delay(delay).ConfigureAwait(false);
            return await _stream.ReadAsync(buffer, offset, count, cancellationToken).ConfigureAwait(false);
        }

        public override async Task WriteAsync(Byte[] buffer, Int32 offset, Int32 count, CancellationToken cancellationToken)
        {
            await Task.Delay(delay).ConfigureAwait(false);
            await _stream.WriteAsync(buffer, offset, count, cancellationToken).ConfigureAwait(false);
        }

        public override Int32 Read(Byte[] buffer, Int32 offset, Int32 count)
        {
            return _stream.Read(buffer, offset, count);
        }

        public override Int64 Seek(Int64 offset, SeekOrigin origin)
        {
            return _stream.Seek(offset, origin);
        }

        public override void SetLength(Int64 value)
        {
            _stream.SetLength(value);
        }

        public override void Write(Byte[] buffer, Int32 offset, Int32 count)
        {
            _stream.Write(buffer, offset, count);
        }
    }
}
