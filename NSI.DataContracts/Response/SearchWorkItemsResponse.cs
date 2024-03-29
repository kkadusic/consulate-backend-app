﻿using NSI.Common.Collation;
using NSI.Common.Collation.Interfaces;
using NSI.DataContracts.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using NSI.Common.DataContracts.Base;

namespace NSI.DataContracts.Response
{
    [ExcludeFromCodeCoverage]
    public class SearchWorkItemsResponse : BaseResponse<IList<WorkItemDto>>, IPageable
    {
        public IList<WorkItemDto> WorkItems { get; set; }
        public Paging Paging { get; set; }
    }
}
