//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.IO;
//using System.IO.Pipelines;
//using System.Threading.Tasks;
//using System.Buffers;

//namespace System.IO.Pipelines // https://sakno.github.io/dotNext/features/io/buffers.html
//{
//    public class MemoryBufferWriter : IBufferWriter<byte>, IDisposable
//    {
//        private BufferSegment _writeHead;
//        private BufferSegment _writeStart;
//        private int _written;
//        private long _committed;

//        private const int MinimumBufferSize = 256;

//        public MemoryBufferWriter(int initialCapacity = MinimumBufferSize)
//        {
//            if (initialCapacity <= 0)
//                throw new ArgumentException(nameof(initialCapacity));

//            _writeHead = new BufferSegment();
//            _writeHead.SetMemory(MemoryPool<byte>.Shared.Rent(initialCapacity));

//            _writeStart = _writeHead;

//            _written = 0;
//            _committed = 0;
//        }

//        public int BytesWritten
//        {
//            get
//            {
//                CheckIfDisposed();

//                return _written;
//            }
//        }

//        public long BytesCommitted
//        {
//            get
//            {
//                CheckIfDisposed();

//                return _committed;
//            }
//        }

//        public void Clear()
//        {
//            CheckIfDisposed();

//            ClearHelper();
//        }

//        private void ClearHelper()
//        {
//            _writeHead.ResetMemory();
//            _written = 0;

//            _writeHead = new BufferSegment();
//            _writeHead.SetMemory(MemoryPool<byte>.Shared.Rent(4_096));

//            _writeStart = _writeHead;
//        }

//        public async Task CopyToAsync(Stream stream, CancellationToken cancellationToken = default)
//        {
//            CheckIfDisposed();

//            if (stream == null)
//                throw new ArgumentNullException(nameof(stream));

//            var sequence = new ReadOnlySequence<byte>(_writeStart, 0, _writeHead, _writeHead.End);

//            await stream.WriteAsync(sequence.ToArray(), cancellationToken).ConfigureAwait(false);
//            _committed += _written;

//            ClearHelper();
//        }

//        public void CopyTo(Stream stream)
//        {
//            CheckIfDisposed();

//            if (stream == null)
//                throw new ArgumentNullException(nameof(stream));

//            var sequence = new ReadOnlySequence<byte>(_writeStart, 0, _writeHead, _writeHead.End);

//            stream.Write(sequence.ToArray());
//            _committed += _written;

//            ClearHelper();
//        }

//        public void Advance(int count)
//        {
//            CheckIfDisposed();

//            if (count < 0)
//                throw new ArgumentException(nameof(count));

//            if (count >= 0)
//            {
//                Memory<byte> buffer = _writeHead.AvailableMemory;

//                if (_writeHead.End > buffer.Length - count)
//                {
//                    throw new InvalidOperationException("Cannot advance past the end of the buffer.");
//                }

//                // if bytesWritten is zero, these do nothing
//                _writeHead.End += count;
//                _written += count;
//            }
//        }

//        // Returns the rented buffer back to the pool
//        public void Dispose()
//        {
//            if (_writeStart == null)
//            {
//                return;
//            }

//            _writeHead.ResetMemory();
//            _written = 0;
//            _writeHead = null;
//            _writeStart = null;
//        }

//        private void CheckIfDisposed()
//        {
//            if (_writeStart == null)
//                throw new ObjectDisposedException(nameof(ArrayBufferWriter));
//        }

//        public Memory<byte> GetMemory(int sizeHint = 0)
//        {
//            CheckIfDisposed();

//            if (sizeHint < 0)
//                throw new ArgumentException(nameof(sizeHint));

//            CheckAndResizeBuffer(sizeHint);

//            int end = _writeHead.End;
//            Memory<byte> availableMemory = _writeHead.AvailableMemory;
//            return availableMemory.Slice(end);
//        }

//        public Span<byte> GetSpan(int sizeHint = 0)
//        {
//            CheckIfDisposed();

//            if (sizeHint < 0)
//                throw new ArgumentException(nameof(sizeHint));

//            CheckAndResizeBuffer(sizeHint);

//            int end = _writeHead.End;
//            Span<byte> availableMemory = _writeHead.AvailableMemory.Span;
//            return availableMemory.Slice(end);
//        }

//        private void CheckAndResizeBuffer(int sizeHint)
//        {
//            Debug.Assert(sizeHint >= 0);

//            if (sizeHint == 0)
//            {
//                sizeHint = MinimumBufferSize;
//            }

//            int bytesLeftInBuffer = _writeHead.WritableBytes;

//            if (sizeHint > bytesLeftInBuffer)
//            {
//                BufferSegment newSegment = new BufferSegment();
//                int newSize = sizeHint > _writeHead.AvailableMemory.Length ? sizeHint : _writeHead.AvailableMemory.Length;
//                newSegment.SetMemory(MemoryPool<byte>.Shared.Rent(newSize));

//                _writeHead.SetNext(newSegment);
//                _writeHead = newSegment;
//            }
//        }
//    }
//}
