﻿namespace LearnTrack.Application.Exceptions;

public class ForbiddenException(string message = "Forbidden") : Exception(message)
{
}
