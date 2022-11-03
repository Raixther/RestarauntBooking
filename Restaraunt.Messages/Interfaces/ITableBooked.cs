

namespace Restaraunt.Messages
{
	public interface ITableBooked
	{
        public Guid OrderId { get; }

        public Guid ClientId { get; }

        public Dish? PreOrder { get; }

        public bool Success { get; }

        public int TableId{ get; }

    }
}