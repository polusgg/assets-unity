namespace Editor.Accounts.Api.Response
{
    public class GenericResponse<T> where T : new()
    {
        public bool Success { get; set; } = false;
        public T Data { get; set; } = new T();
    }
}