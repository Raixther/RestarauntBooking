using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RestarauntBooking
{
	public class Table
	{

		private System.Timers.Timer? _timer = null;

		private readonly Random _rand = new();
		public int Id { get; }

		public State State { get; private set; }

		public int SeatsCount { get; }

		public Table(int id)
		{
			Id = id;
			State = State.Free;
			SeatsCount = _rand.Next(2, 5);

		}

		public bool SetState(State state)
		{
			if (state == State)
				return false;
			State = state;
			if (State ==State.Booked)
			{
				_timer = new(20000);
				_timer.Elapsed+= OnTimedEvent;
				_timer.AutoReset = false;
				_timer.Enabled = true;
			}
			return true;
		}

		private void OnTimedEvent(object source, System.Timers.ElapsedEventArgs e)
		{
			SetState(State.Free);
			Console.WriteLine($"Бронирование столика {Id} истекло") ;
		}
		
	
	}
	public enum State
	{
		Free = 0,
		Booked = 1,
	}
}
