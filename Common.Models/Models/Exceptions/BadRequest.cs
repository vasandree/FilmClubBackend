namespace Common.Models.Models.Exceptions;

public class BadRequest(string? message) : Exception(message);