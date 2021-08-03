namespace Editor.Accounts.Api.Response {
    public class GenericCosmeticResponse<T> {
        public bool Ok { get; set; } = false;
        public T Data { get; set; }
        public string Cause;
    }
}