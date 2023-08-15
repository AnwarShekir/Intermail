using System;
namespace Intermail.Dto
{
	public class ExternalRequestDto
	{
		public int Amount { get; private set; }
		public string Source { get; } = "AutomationFixedLoyaltyPoints";

        public ExternalRequestDto(int amount)
		{
			Amount = amount;
		}
	}
}

