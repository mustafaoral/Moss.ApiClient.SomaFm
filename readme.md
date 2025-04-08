# Moss API Client for Soma FM

[![Build Status](https://dev.azure.com/mossmoss/Moss.ApiClient.SomaFm/_apis/build/status/Moss.ApiClient.SomaFm?branchName=main)](https://dev.azure.com/mossmoss/Moss.ApiClient.SomaFm/_build/latest?definitionId=25&branchName=main)
[![NuGet](https://img.shields.io/nuget/v/Moss.ApiClient.SomaFm.svg?style=flat)](https://www.nuget.org/packages/Moss.ApiClient.SomaFm)

An API client for Soma FM. Provides methods to retrive data about channels and recently played songs.

## Soma FM API Usage

### `SomaFmApiClient.RetrieveChannels`
Retrieves data from `https://api.somafm.com/channels.json`.

### `SomaFmApiClient.RetrieveRecentlyPlayedSongs`
Retrieves data from `https://api.somafm.com/songs/[channelId].json`.

## Limitations

### `SomaFmApiClient.RetrieveRecentlyPlayedSongs`
The data retrieved from `https://api.somafm.com/songs/[channelId].json` includes an `albumart` property, but it is always set as empty. As a result, `RecentlyPlayedSong` does not include such a property. 

## Moss.ApiClient.SomaFm.Extensions package
This package provides a single extension method for `RecentlyPlayedSong`, which generates a formatted string representation of an instance.
