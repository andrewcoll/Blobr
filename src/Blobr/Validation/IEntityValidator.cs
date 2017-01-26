namespace Blobr.Validation
{
    public interface IEntityValidator<T>
    {
        ValidationResult Validate(T entity);
    }
}