using Backend.Models;
using Dapper;
using System.Data;

namespace Backend.Data;

public class WeatherHashMapRepository : IWeatherRepository
{
    private readonly IDbConnection _db;
    private const int BUCKET_COUNT = 1000;

    public WeatherHashMapRepository(IDbConnection db)
    {
        _db = db;
    }

     
    private int GetBucketIndex(string key)
    {
         
        return Math.Abs(key.GetHashCode()) % BUCKET_COUNT;
    }

    public async Task<string?> GetCachedWeatherAsync(string city)
    {
         
        int bucketIndex = GetBucketIndex(city);
        
         
        int bucketId = bucketIndex; 

         
         
        var query = "SELECT * FROM HashEntry WHERE BucketId = @BucketId";
        var entries = (await _db.QueryAsync<HashEntry>(query, new { BucketId = bucketId })).ToList();

         
         
         
        
         
         
         
         
         
        
        foreach (var entry in entries)
        {
            if (entry.KeyValue.Equals(city, StringComparison.OrdinalIgnoreCase))
            {
                 
                return entry.DataJson;
            }
        }

         
        return null;
    }

    public async Task CacheWeatherAsync(string city, string json)
    {
        int bucketId = GetBucketIndex(city);

         
        if (_db.State != ConnectionState.Open) _db.Open();
        using var transaction = _db.BeginTransaction();

        try
        {
             
             
            
             
             
             
             
             
            
            var tailQuery = "SELECT TOP 1 * FROM HashEntry WHERE BucketId = @BucketId AND NextEntryId IS NULL";
            var tail = await _db.QueryFirstOrDefaultAsync<HashEntry>(tailQuery, new { BucketId = bucketId }, transaction);

             
            var insertQuery = @"
                INSERT INTO HashEntry (BucketId, KeyValue, DataJson, NextEntryId) 
                OUTPUT INSERTED.EntryId
                VALUES (@BucketId, @KeyValue, @DataJson, NULL)";
            
            int newEntryId = await _db.QuerySingleAsync<int>(insertQuery, new 
            { 
                BucketId = bucketId, 
                KeyValue = city, 
                DataJson = json 
            }, transaction);

             
            if (tail != null)
            {
                var updateQuery = "UPDATE HashEntry SET NextEntryId = @NewNextId WHERE EntryId = @OldTailId";
                await _db.ExecuteAsync(updateQuery, new { NewNextId = newEntryId, OldTailId = tail.EntryId }, transaction);
            }

            transaction.Commit();
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }

    public async Task LogRequestAsync(string city, bool isHit)
    {
         
        try 
        {
             var sql = @"
                INSERT INTO Request (UserId, RequestType, QueryKey) VALUES (1, 'Weather', @City);
                DECLARE @ReqId INT = SCOPE_IDENTITY();
                INSERT INTO CacheLog (RequestId, HitOrMiss) VALUES (@ReqId, @HitOrMiss);";
             
             await _db.ExecuteAsync(sql, new { City = city, HitOrMiss = isHit ? "HIT" : "MISS" });
        }
        catch 
        {
             
        }
    }
}
