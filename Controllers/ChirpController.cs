using Microsoft.EntityFrameworkCore;
using Chirp.Models;
using Chirp.Database;

namespace Chirp.Controller;

public static class ChirpEndpoints
{
    public static void Map(WebApplication app)
    {
        // Retrieve all chirps data in the database
        app.MapGet("/getchirps", async (ChirpDb db) => {
            List<ChirpData> Chirps= await db.Chirps.ToListAsync();

            return Results.Ok(Chirps);
        });

        // Store new chirp data inside database
        app.MapPost("/postchirp", async (ChirpDb db, ChirpData chirp) => {
            await db.Chirps.AddAsync(chirp);
            await db.SaveChangesAsync();

            return Results.Created($"/chirp/{chirp.Id}", chirp);
        });

        // Delete chirp data from the database
        app.MapDelete("/deletechirp/{id}", async (ChirpDb db, int id) => {
            // Find whether the chirp data exists in the database
            if (await db.Chirps.FindAsync(id) is ChirpData chirp)
            {
                db.Chirps.Remove(chirp);
                await db.SaveChangesAsync();

                return Results.Ok(chirp);
            }

            return Results.NotFound();
        });
    }
}