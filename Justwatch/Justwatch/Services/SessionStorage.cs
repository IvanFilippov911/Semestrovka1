namespace Justwatch.Services;

public static class SessionStorage
{
    private static readonly Dictionary<string, string> _sessions = new Dictionary<string, string>();

    public static void SaveSession(string token, string userId)
    {
        _sessions[token] = userId;
    }

    public static bool ValidateToken(string token)
    {
        return _sessions.ContainsKey(token);
    }

    public static string GetUserId(string token)
    {
        return _sessions.TryGetValue(token, out var userId) ? userId : null;
    }
}
