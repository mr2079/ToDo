﻿namespace Domain.Entities.Abstractions;

public interface ISoftDeletable
{
    bool IsDeleted { get; set; }
}