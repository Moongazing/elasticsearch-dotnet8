﻿namespace Moongazing.ElasticSearch.Models;

public class ElasticSearchInsertUpdateModel : ElasticSearchModel
{
    public object Item { get; set; } = default!;
}