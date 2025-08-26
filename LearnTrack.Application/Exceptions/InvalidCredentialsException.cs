namespace LearnTrack.Application.Exceptions;

public class InvalidCredentialsException(string message = "Invalid credentials") : Exception(message);
