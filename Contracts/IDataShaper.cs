using System.Dynamic;
namespace Contracts
{
    public interface IDataShaper<T>
    {
        // this method is implemented for multiple entities
        IEnumerable<ExpandoObject> ShapeData(IEnumerable<T> entities , string fieldsString);

        // this method is implemented for single entity 
        ExpandoObject ShapeData(T entity , string fieldsString);
    }
}
