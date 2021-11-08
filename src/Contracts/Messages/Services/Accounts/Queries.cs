﻿using System;
using Messages.Abstractions.Queries;

namespace Messages.Services.Accounts;

public static class Queries
{
    public record GetAccountDetails(Guid AccountId) : Query;

    public record GetAccountsDetailsWithPagination(int Limit, int Offset) : QueryPaging(Limit, Offset);
}