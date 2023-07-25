namespace mvcCRUD.Repository.Interfaces;

public interface IGenericRepository<T> where T : class
{
    Task<List<T>> ModelList();

    Task<bool> NewMemberModel(T model);

    Task<bool> EditMemberModel(T model);

    Task<bool> DeleteMemberModel(int model);
}
