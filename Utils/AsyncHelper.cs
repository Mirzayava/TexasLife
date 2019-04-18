using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GTANetworkAPI;
using MongoDB.Bson;
using MongoDB.Driver;

namespace TexasLife.Utils
{
    //public static class AsyncHelper
    //{
    //    public static Task<T> RunAsync<T>(Func<T> callback)
    //    {
    //        var taskCompletion = new TaskCompletionSource<T>();

    //        NAPI.Task.Run(() =>
    //        {
    //            try
    //            {
    //                taskCompletion.TrySetResult(callback());
    //            }
    //            catch (OperationCanceledException)
    //            {
    //                taskCompletion.TrySetCanceled();
    //            }
    //            catch (Exception ex)
    //            {
    //                taskCompletion.TrySetException(ex);
    //            }
    //        });

    //        return taskCompletion.Task;
    //    }

    //    public static Task<T> RunAsync<T>(this GTANetworkMethods.Task gtanTask, Func<T> callback) =>
    //        RunAsync(callback);

    //    public static async Task<ReplaceOneResult> SaveAsync<T>(this IMongoCollection<T> collection, T entity) where T : IIdentified
    //    {
    //        return await collection.ReplaceOneAsync(i => i.Id == entity.Id, entity, new UpdateOptions { IsUpsert = true });
    //    }

    //    public interface IIdentified
    //    {
    //        ObjectId Id { get; }
    //    }
    //}
}
