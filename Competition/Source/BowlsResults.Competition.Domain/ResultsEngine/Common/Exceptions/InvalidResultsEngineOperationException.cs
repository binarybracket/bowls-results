﻿using System;

 namespace Com.BinaryBracket.BowlsResults.Competition.Domain.ResultsEngine.Common.Exceptions
{
	public class InvalidResultsEngineOperationException : InvalidOperationException
	{
		public InvalidResultsEngineOperationException(string msg) : base(msg)
		{
			
		}
	}
}