﻿namespace LearnTrack.Application.Exceptions;

public class NotFoundException(string message = "Resource not found") : Exception(message)
{
}