
module iTunesExt

open System
open System.IO
open iTunes

type IITArtworkCollection with
  member this.Seq
    with get() : (seq<IITArtwork>) = this |> Seq.cast

type IITEncoderCollection with
  member this.Seq
    with get() : (seq<IITEncoder>) = this |> Seq.cast

type IITEQPresetCollection with
  member this.Seq
    with get() : (seq<IITEQPreset>) = this |> Seq.cast

type IITPlaylistCollection with
  member this.Seq
    with get() : (seq<IITPlaylist>) = this |> Seq.cast

type IITSourceCollection with
  member this.Seq
    with get() : (seq<IITSource>) = this |> Seq.cast

type IITTrackCollection with
  member this.Seq
    with get() : (seq<IITTrack>) = this |> Seq.cast
  
  /// only tracks that are files
  member this.Files
    with get() =
      this.Seq
      |> Seq.filter (fun t ->  t.Kind = ITTrackKind.ITTrackKindFile)
      |> Seq.map (fun t -> t :?> IITFileOrCDTrack)

type IITVisualCollection with
  member this.Seq
    with get() : (seq<IITVisual>) = this |> Seq.cast

type IITWindowCollection with
  member this.Seq
    with get() : (seq<IITWindow>) = this |> Seq.cast

type IiTunes with
  member this.Playlists
    with get() = this.Sources.Seq |> Seq.map (fun s -> s.Playlists.Seq) |> Seq.concat

  /// only user playlists that are not a special kind
  member this.UserPlaylists
    with get() =
      this.Playlists
      |> Seq.filter (fun p ->  p.Kind = ITPlaylistKind.ITPlaylistKindUser)
      |> Seq.map (fun p -> p :?> IITUserPlaylist)
      |> Seq.filter (fun p -> p.SpecialKind = ITUserPlaylistSpecialKind.ITUserPlaylistSpecialKindNone)
