
#r "iTunes.Net.dll"
#r @"bin\Debug\iTunesExt.dll"

open System
open System.IO
open iTunes
open iTunesExt

// calling this will launch iTunes if not already open
let app = iTunesAppClass()

// see what verion of iTunes your running
app.Version

// control the iTunes app
app.Play()
app.Stop()
app.NextTrack()
app.BackTrack()
app.Mute <- true
app.Mute <- false
app.BrowserWindow.MiniPlayer <- true
app.BrowserWindow.MiniPlayer <- false

// print all playlists
app.Playlists
|> Seq.iter (fun p -> printfn "%s" p.Name)

// print the user playlists that are not smart
app.UserPlaylists
|> Seq.filter (fun p -> false = p.Smart)
|> Seq.iter(fun p -> printfn "%s" p.Name)

// get a list of tracks that are missing files
// they are marked with an '!' in the iTunes UI
let tracksMissingFiles =
  app.LibraryPlaylist.Tracks.Files
  |> Seq.filter (fun t -> false = File.Exists t.Location)
  |> List.ofSeq

// see how many were found
tracksMissingFiles.Length

// print the list
tracksMissingFiles |> Seq.iter (fun t -> printfn "%s" t.Name)

// remove the tracks from iTunes
tracksMissingFiles |> Seq.iter (fun t -> t.Delete())