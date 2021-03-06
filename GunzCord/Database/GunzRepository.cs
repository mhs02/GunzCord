﻿using Dapper;
using GunzCord.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GunzCord.Database
{
	public class GunzRepository : IGunzRepository
	{
		private readonly IDatabaseService _databaseService;

		public GunzRepository(IDatabaseService databaseService)
		{
			_databaseService = databaseService;
		}

		public async Task<IEnumerable<ServerStatus>> GetServerStatusAsync()
		{
			IEnumerable<ServerStatus> result = null;

			using (var transaction = _databaseService.Connection.BeginTransaction())
			{
				result = await _databaseService.Connection.QueryAsync<ServerStatus>("[dbo].[spGetServerStatus]", transaction: transaction, commandType: System.Data.CommandType.StoredProcedure);
			}

			return result;
		}
	}
}
