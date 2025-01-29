﻿namespace Domain.Entities.Abstractions;

public interface IAuditable
{
    DateTime CreatedAt { get; set; }
    DateTime? ModifiedAt { get; set; }
}