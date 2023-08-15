using System;
using Intermail.Dto;

namespace Intermail.Services
{
	public interface IExternalService
	{
		Task SendLoyaltyPoint(RequestDto request);
	}
}

