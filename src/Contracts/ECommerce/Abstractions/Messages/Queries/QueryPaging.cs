﻿using System;
using ECommerce.Abstractions.Messages.Queries.Paging;
using MassTransit;

namespace ECommerce.Abstractions.Messages.Queries;

[ExcludeFromTopology]
public abstract record QueryPaging(int Limit, int Offset, Guid CorrelationId = default) : Query(CorrelationId), IPaging;