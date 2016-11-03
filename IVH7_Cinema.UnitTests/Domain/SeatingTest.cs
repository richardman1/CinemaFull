using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using IVH7_Cinema.Domain.Entities;
using IVH7_Cinema.Domain.Abstract;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using IVH7_Cinema.WebUI.HtmlHelpers;
using IVH7_Cinema.WebUI.Models;
using System.Diagnostics.CodeAnalysis;

namespace IVH7_Cinema.UnitTests
{
    [TestClass][ExcludeFromCodeCoverage]
    public class SeatingTest
    {
        //Screen
        List<Screen> TestScreens = new List<Screen> { new Screen()
            { 
                Size = 45,
                ScreenID = 1,
                Seats = new List<Seat> {                      new Seat { ScreenID = 1, RowNumber = 1, SeatNumber = 1, Vacated = false, SeatID = 1},
                                                              new Seat { ScreenID = 1, RowNumber = 1, SeatNumber = 2, Vacated = false, SeatID = 2},
                                                              new Seat { ScreenID = 1, RowNumber = 1, SeatNumber = 3, Vacated = false, SeatID = 3},
                                                              new Seat { ScreenID = 1, RowNumber = 1, SeatNumber = 4, Vacated = false, SeatID = 4},
                                                              new Seat { ScreenID = 1, RowNumber = 1, SeatNumber = 5, Vacated = false , SeatID = 5},
                                                              new Seat { ScreenID = 1, RowNumber = 1, SeatNumber = 6, Vacated = false , SeatID = 6},
                                                              new Seat { ScreenID = 1, RowNumber = 1, SeatNumber = 7, Vacated = false , SeatID = 7},
                                                              new Seat { ScreenID = 1, RowNumber = 1, SeatNumber = 8, Vacated = false , SeatID = 8},
                                                              new Seat { ScreenID = 1, RowNumber = 1, SeatNumber = 9, Vacated = false , SeatID = 9},
                                                              new Seat { ScreenID = 1, RowNumber = 1, SeatNumber = 10, Vacated = false , SeatID = 10},
                                                              new Seat { ScreenID = 1, RowNumber = 1, SeatNumber = 11, Vacated = false , SeatID = 11},
                                                              new Seat { ScreenID = 1, RowNumber = 1, SeatNumber = 12, Vacated = false , SeatID = 12},
                                                              new Seat { ScreenID = 1, RowNumber = 1, SeatNumber = 13, Vacated = false , SeatID = 13},
                                                              new Seat { ScreenID = 1, RowNumber = 1, SeatNumber = 14, Vacated = false , SeatID = 14},
                                                              new Seat { ScreenID = 1, RowNumber = 1, SeatNumber = 15, Vacated = false , SeatID = 15},
                                                              new Seat { ScreenID = 1, RowNumber = 2, SeatNumber = 1, Vacated = false , SeatID = 16},
                                                              new Seat { ScreenID = 1, RowNumber = 2, SeatNumber = 2, Vacated = false , SeatID = 17},
                                                              new Seat { ScreenID = 1, RowNumber = 2, SeatNumber = 3, Vacated = false , SeatID = 18},
                                                              new Seat { ScreenID = 1, RowNumber = 2, SeatNumber = 4, Vacated = false , SeatID = 19},
                                                              new Seat { ScreenID = 1, RowNumber = 2, SeatNumber = 5, Vacated = false , SeatID = 20},
                                                              new Seat { ScreenID = 1, RowNumber = 2, SeatNumber = 6, Vacated = false , SeatID = 21},
                                                              new Seat { ScreenID = 1, RowNumber = 2, SeatNumber = 7, Vacated = false , SeatID = 22},
                                                              new Seat { ScreenID = 1, RowNumber = 2, SeatNumber = 8, Vacated = false , SeatID = 23},
                                                              new Seat { ScreenID = 1, RowNumber = 2, SeatNumber = 9, Vacated = false , SeatID = 24},
                                                              new Seat { ScreenID = 1, RowNumber = 2, SeatNumber = 10, Vacated = false , SeatID = 25},
                                                              new Seat { ScreenID = 1, RowNumber = 2, SeatNumber = 11, Vacated = false , SeatID = 26},
                                                              new Seat { ScreenID = 1, RowNumber = 2, SeatNumber = 12, Vacated = false , SeatID = 27},
                                                              new Seat { ScreenID = 1, RowNumber = 2, SeatNumber = 13, Vacated = false , SeatID = 28},
                                                              new Seat { ScreenID = 1, RowNumber = 2, SeatNumber = 14, Vacated = false , SeatID = 29},
                                                              new Seat { ScreenID = 1, RowNumber = 2, SeatNumber = 15, Vacated = false , SeatID = 30},
                                                              new Seat { ScreenID = 1, RowNumber = 3, SeatNumber = 1, Vacated = false , SeatID = 31},
                                                              new Seat { ScreenID = 1, RowNumber = 3, SeatNumber = 2, Vacated = false , SeatID = 32},
                                                              new Seat { ScreenID = 1, RowNumber = 3, SeatNumber = 3, Vacated = false , SeatID = 33},
                                                              new Seat { ScreenID = 1, RowNumber = 3, SeatNumber = 4, Vacated = false , SeatID = 34},
                                                              new Seat { ScreenID = 1, RowNumber = 3, SeatNumber = 5, Vacated = false , SeatID = 35},
                                                              new Seat { ScreenID = 1, RowNumber = 3, SeatNumber = 6, Vacated = false , SeatID = 36},
                                                              new Seat { ScreenID = 1, RowNumber = 3, SeatNumber = 7, Vacated = false , SeatID = 37},
                                                              new Seat { ScreenID = 1, RowNumber = 3, SeatNumber = 8, Vacated = false , SeatID = 38},
                                                              new Seat { ScreenID = 1, RowNumber = 3, SeatNumber = 9, Vacated = false , SeatID = 39},
                                                              new Seat { ScreenID = 1, RowNumber = 3, SeatNumber = 10, Vacated = false , SeatID = 40},
                                                              new Seat { ScreenID = 1, RowNumber = 3, SeatNumber = 11, Vacated = false , SeatID = 41},
                                                              new Seat { ScreenID = 1, RowNumber = 3, SeatNumber = 12, Vacated = false , SeatID = 42},
                                                              new Seat { ScreenID = 1, RowNumber = 3, SeatNumber = 13, Vacated = false , SeatID = 43},
                                                              new Seat { ScreenID = 1, RowNumber = 3, SeatNumber = 14, Vacated = false , SeatID = 44},
                                                              new Seat { ScreenID = 1, RowNumber = 3, SeatNumber = 15, Vacated = false , SeatID = 45} 
                } }};

        //Show
        List<Show> TestShows = new List<Show> { new Show() 
                    {
                        ShowID =1,
                        MovieID = 1,
                        Screen =  new Screen()
            { 
                Size = 45,
                ScreenID = 1,
                Seats = new List<Seat> {                      new Seat { ScreenID = 1, RowNumber = 1, SeatNumber = 1, Vacated = false, SeatID = 1},
                                                              new Seat { ScreenID = 1, RowNumber = 1, SeatNumber = 2, Vacated = false, SeatID = 2},
                                                              new Seat { ScreenID = 1, RowNumber = 1, SeatNumber = 3, Vacated = false, SeatID = 3},
                                                              new Seat { ScreenID = 1, RowNumber = 1, SeatNumber = 4, Vacated = false, SeatID = 4},
                                                              new Seat { ScreenID = 1, RowNumber = 1, SeatNumber = 5, Vacated = false , SeatID = 5},
                                                              new Seat { ScreenID = 1, RowNumber = 1, SeatNumber = 6, Vacated = false , SeatID = 6},
                                                              new Seat { ScreenID = 1, RowNumber = 1, SeatNumber = 7, Vacated = false , SeatID = 7},
                                                              new Seat { ScreenID = 1, RowNumber = 1, SeatNumber = 8, Vacated = false , SeatID = 8},
                                                              new Seat { ScreenID = 1, RowNumber = 1, SeatNumber = 9, Vacated = false , SeatID = 9},
                                                              new Seat { ScreenID = 1, RowNumber = 1, SeatNumber = 10, Vacated = false , SeatID = 10},
                                                              new Seat { ScreenID = 1, RowNumber = 1, SeatNumber = 11, Vacated = false , SeatID = 11},
                                                              new Seat { ScreenID = 1, RowNumber = 1, SeatNumber = 12, Vacated = false , SeatID = 12},
                                                              new Seat { ScreenID = 1, RowNumber = 1, SeatNumber = 13, Vacated = false , SeatID = 13},
                                                              new Seat { ScreenID = 1, RowNumber = 1, SeatNumber = 14, Vacated = false , SeatID = 14},
                                                              new Seat { ScreenID = 1, RowNumber = 1, SeatNumber = 15, Vacated = false , SeatID = 15},
                                                              new Seat { ScreenID = 1, RowNumber = 2, SeatNumber = 1, Vacated = false , SeatID = 16},
                                                              new Seat { ScreenID = 1, RowNumber = 2, SeatNumber = 2, Vacated = false , SeatID = 17},
                                                              new Seat { ScreenID = 1, RowNumber = 2, SeatNumber = 3, Vacated = false , SeatID = 18},
                                                              new Seat { ScreenID = 1, RowNumber = 2, SeatNumber = 4, Vacated = false , SeatID = 19},
                                                              new Seat { ScreenID = 1, RowNumber = 2, SeatNumber = 5, Vacated = false , SeatID = 20},
                                                              new Seat { ScreenID = 1, RowNumber = 2, SeatNumber = 6, Vacated = false , SeatID = 21},
                                                              new Seat { ScreenID = 1, RowNumber = 2, SeatNumber = 7, Vacated = false , SeatID = 22},
                                                              new Seat { ScreenID = 1, RowNumber = 2, SeatNumber = 8, Vacated = false , SeatID = 23},
                                                              new Seat { ScreenID = 1, RowNumber = 2, SeatNumber = 9, Vacated = false , SeatID = 24},
                                                              new Seat { ScreenID = 1, RowNumber = 2, SeatNumber = 10, Vacated = false , SeatID = 25},
                                                              new Seat { ScreenID = 1, RowNumber = 2, SeatNumber = 11, Vacated = false , SeatID = 26},
                                                              new Seat { ScreenID = 1, RowNumber = 2, SeatNumber = 12, Vacated = false , SeatID = 27},
                                                              new Seat { ScreenID = 1, RowNumber = 2, SeatNumber = 13, Vacated = false , SeatID = 28},
                                                              new Seat { ScreenID = 1, RowNumber = 2, SeatNumber = 14, Vacated = false , SeatID = 29},
                                                              new Seat { ScreenID = 1, RowNumber = 2, SeatNumber = 15, Vacated = false , SeatID = 30},
                                                              new Seat { ScreenID = 1, RowNumber = 3, SeatNumber = 1, Vacated = false , SeatID = 31},
                                                              new Seat { ScreenID = 1, RowNumber = 3, SeatNumber = 2, Vacated = false , SeatID = 32},
                                                              new Seat { ScreenID = 1, RowNumber = 3, SeatNumber = 3, Vacated = false , SeatID = 33},
                                                              new Seat { ScreenID = 1, RowNumber = 3, SeatNumber = 4, Vacated = false , SeatID = 34},
                                                              new Seat { ScreenID = 1, RowNumber = 3, SeatNumber = 5, Vacated = false , SeatID = 35},
                                                              new Seat { ScreenID = 1, RowNumber = 3, SeatNumber = 6, Vacated = false , SeatID = 36},
                                                              new Seat { ScreenID = 1, RowNumber = 3, SeatNumber = 7, Vacated = false , SeatID = 37},
                                                              new Seat { ScreenID = 1, RowNumber = 3, SeatNumber = 8, Vacated = false , SeatID = 38},
                                                              new Seat { ScreenID = 1, RowNumber = 3, SeatNumber = 9, Vacated = false , SeatID = 39},
                                                              new Seat { ScreenID = 1, RowNumber = 3, SeatNumber = 10, Vacated = false , SeatID = 40},
                                                              new Seat { ScreenID = 1, RowNumber = 3, SeatNumber = 11, Vacated = false , SeatID = 41},
                                                              new Seat { ScreenID = 1, RowNumber = 3, SeatNumber = 12, Vacated = false , SeatID = 42},
                                                              new Seat { ScreenID = 1, RowNumber = 3, SeatNumber = 13, Vacated = false , SeatID = 43},
                                                              new Seat { ScreenID = 1, RowNumber = 3, SeatNumber = 14, Vacated = false , SeatID = 44},
                                                              new Seat { ScreenID = 1, RowNumber = 3, SeatNumber = 15, Vacated = false , SeatID = 45} 
                }},
                        CinemaID = 1,
                        LanguageID = 1,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddHours(14).AddMinutes(30)
                    },  new Show() 
                    {
                        ShowID =1,
                        MovieID = 1,
                        Screen =  new Screen()
            { 
                Size = 45,
                ScreenID = 1,
                Seats = new List<Seat> {                      new Seat { ScreenID = 1, RowNumber = 1, SeatNumber = 1, Vacated = false, SeatID = 1},
                                                              new Seat { ScreenID = 1, RowNumber = 1, SeatNumber = 2, Vacated = false, SeatID = 2},
                                                              new Seat { ScreenID = 1, RowNumber = 1, SeatNumber = 3, Vacated = false, SeatID = 3},
                                                              new Seat { ScreenID = 1, RowNumber = 1, SeatNumber = 4, Vacated = false, SeatID = 4},
                                                              new Seat { ScreenID = 1, RowNumber = 1, SeatNumber = 5, Vacated = false , SeatID = 5},
                                                              new Seat { ScreenID = 1, RowNumber = 1, SeatNumber = 6, Vacated = false , SeatID = 6},
                                                              new Seat { ScreenID = 1, RowNumber = 1, SeatNumber = 7, Vacated = false , SeatID = 7},
                                                              new Seat { ScreenID = 1, RowNumber = 1, SeatNumber = 8, Vacated = false , SeatID = 8},
                                                              new Seat { ScreenID = 1, RowNumber = 1, SeatNumber = 9, Vacated = false , SeatID = 9},
                                                              new Seat { ScreenID = 1, RowNumber = 1, SeatNumber = 10, Vacated = false , SeatID = 10},
                                                              new Seat { ScreenID = 1, RowNumber = 1, SeatNumber = 11, Vacated = false , SeatID = 11},
                                                              new Seat { ScreenID = 1, RowNumber = 1, SeatNumber = 12, Vacated = false , SeatID = 12},
                                                              new Seat { ScreenID = 1, RowNumber = 1, SeatNumber = 13, Vacated = false , SeatID = 13},
                                                              new Seat { ScreenID = 1, RowNumber = 1, SeatNumber = 14, Vacated = false , SeatID = 14},
                                                              new Seat { ScreenID = 1, RowNumber = 1, SeatNumber = 15, Vacated = false , SeatID = 15},
                                                              new Seat { ScreenID = 1, RowNumber = 2, SeatNumber = 1, Vacated = false , SeatID = 16},
                                                              new Seat { ScreenID = 1, RowNumber = 2, SeatNumber = 2, Vacated = false , SeatID = 17},
                                                              new Seat { ScreenID = 1, RowNumber = 2, SeatNumber = 3, Vacated = false , SeatID = 18},
                                                              new Seat { ScreenID = 1, RowNumber = 2, SeatNumber = 4, Vacated = false , SeatID = 19},
                                                              new Seat { ScreenID = 1, RowNumber = 2, SeatNumber = 5, Vacated = false , SeatID = 20},
                                                              new Seat { ScreenID = 1, RowNumber = 2, SeatNumber = 6, Vacated = false , SeatID = 21},
                                                              new Seat { ScreenID = 1, RowNumber = 2, SeatNumber = 7, Vacated = false , SeatID = 22},
                                                              new Seat { ScreenID = 1, RowNumber = 2, SeatNumber = 8, Vacated = false , SeatID = 23},
                                                              new Seat { ScreenID = 1, RowNumber = 2, SeatNumber = 9, Vacated = false , SeatID = 24},
                                                              new Seat { ScreenID = 1, RowNumber = 2, SeatNumber = 10, Vacated = false , SeatID = 25},
                                                              new Seat { ScreenID = 1, RowNumber = 2, SeatNumber = 11, Vacated = false , SeatID = 26},
                                                              new Seat { ScreenID = 1, RowNumber = 2, SeatNumber = 12, Vacated = false , SeatID = 27},
                                                              new Seat { ScreenID = 1, RowNumber = 2, SeatNumber = 13, Vacated = false , SeatID = 28},
                                                              new Seat { ScreenID = 1, RowNumber = 2, SeatNumber = 14, Vacated = false , SeatID = 29},
                                                              new Seat { ScreenID = 1, RowNumber = 2, SeatNumber = 15, Vacated = false , SeatID = 30},
                                                              new Seat { ScreenID = 1, RowNumber = 3, SeatNumber = 1, Vacated = false , SeatID = 31},
                                                              new Seat { ScreenID = 1, RowNumber = 3, SeatNumber = 2, Vacated = false , SeatID = 32},
                                                              new Seat { ScreenID = 1, RowNumber = 3, SeatNumber = 3, Vacated = false , SeatID = 33},
                                                              new Seat { ScreenID = 1, RowNumber = 3, SeatNumber = 4, Vacated = false , SeatID = 34},
                                                              new Seat { ScreenID = 1, RowNumber = 3, SeatNumber = 5, Vacated = false , SeatID = 35},
                                                              new Seat { ScreenID = 1, RowNumber = 3, SeatNumber = 6, Vacated = false , SeatID = 36},
                                                              new Seat { ScreenID = 1, RowNumber = 3, SeatNumber = 7, Vacated = false , SeatID = 37},
                                                              new Seat { ScreenID = 1, RowNumber = 3, SeatNumber = 8, Vacated = false , SeatID = 38},
                                                              new Seat { ScreenID = 1, RowNumber = 3, SeatNumber = 9, Vacated = false , SeatID = 39},
                                                              new Seat { ScreenID = 1, RowNumber = 3, SeatNumber = 10, Vacated = false , SeatID = 40},
                                                              new Seat { ScreenID = 1, RowNumber = 3, SeatNumber = 11, Vacated = false , SeatID = 41},
                                                              new Seat { ScreenID = 1, RowNumber = 3, SeatNumber = 12, Vacated = false , SeatID = 42},
                                                              new Seat { ScreenID = 1, RowNumber = 3, SeatNumber = 13, Vacated = false , SeatID = 43},
                                                              new Seat { ScreenID = 1, RowNumber = 3, SeatNumber = 14, Vacated = false , SeatID = 44},
                                                              new Seat { ScreenID = 1, RowNumber = 3, SeatNumber = 15, Vacated = false , SeatID = 45} 
                }},
                        CinemaID = 1,
                        LanguageID = 1,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddHours(14).AddMinutes(30),
                        Tickets = new List<Ticket> {new Ticket{ OrderID = 1}}
                    },
        new Show() 
                    {
                        ShowID =1,
                        MovieID = 1,
                        Screen =  new Screen()
            { 
                Size = 45,
                ScreenID = 1,
                Seats = new List<Seat> {                      new Seat { ScreenID = 1, RowNumber = 1, SeatNumber = 1, Vacated = false, SeatID = 1},
                                                              new Seat { ScreenID = 1, RowNumber = 1, SeatNumber = 2, Vacated = false, SeatID = 2},
                                                              new Seat { ScreenID = 1, RowNumber = 1, SeatNumber = 3, Vacated = false, SeatID = 3},
                                                              new Seat { ScreenID = 1, RowNumber = 1, SeatNumber = 4, Vacated = false, SeatID = 4},
                                                              new Seat { ScreenID = 1, RowNumber = 1, SeatNumber = 5, Vacated = false , SeatID = 5},
                                                              new Seat { ScreenID = 1, RowNumber = 1, SeatNumber = 6, Vacated = false , SeatID = 6},
                                                              new Seat { ScreenID = 1, RowNumber = 1, SeatNumber = 7, Vacated = false , SeatID = 7},
                                                              new Seat { ScreenID = 1, RowNumber = 1, SeatNumber = 8, Vacated = false , SeatID = 8},
                                                              new Seat { ScreenID = 1, RowNumber = 1, SeatNumber = 9, Vacated = false , SeatID = 9},
                                                              new Seat { ScreenID = 1, RowNumber = 1, SeatNumber = 10, Vacated = false , SeatID = 10},
                                                              new Seat { ScreenID = 1, RowNumber = 1, SeatNumber = 11, Vacated = false , SeatID = 11},
                                                              new Seat { ScreenID = 1, RowNumber = 1, SeatNumber = 12, Vacated = false , SeatID = 12},
                                                              new Seat { ScreenID = 1, RowNumber = 1, SeatNumber = 13, Vacated = false , SeatID = 13},
                                                              new Seat { ScreenID = 1, RowNumber = 1, SeatNumber = 14, Vacated = false , SeatID = 14},
                                                              new Seat { ScreenID = 1, RowNumber = 1, SeatNumber = 15, Vacated = false , SeatID = 15},
                                                              new Seat { ScreenID = 1, RowNumber = 2, SeatNumber = 1, Vacated = false , SeatID = 16},
                                                              new Seat { ScreenID = 1, RowNumber = 2, SeatNumber = 2, Vacated = false , SeatID = 17},
                                                              new Seat { ScreenID = 1, RowNumber = 2, SeatNumber = 3, Vacated = false , SeatID = 18},
                                                              new Seat { ScreenID = 1, RowNumber = 2, SeatNumber = 4, Vacated = false , SeatID = 19},
                                                              new Seat { ScreenID = 1, RowNumber = 2, SeatNumber = 5, Vacated = false , SeatID = 20},
                                                              new Seat { ScreenID = 1, RowNumber = 2, SeatNumber = 6, Vacated = false , SeatID = 21},
                                                              new Seat { ScreenID = 1, RowNumber = 2, SeatNumber = 7, Vacated = false , SeatID = 22},
                                                              new Seat { ScreenID = 1, RowNumber = 2, SeatNumber = 8, Vacated = false , SeatID = 23},
                                                              new Seat { ScreenID = 1, RowNumber = 2, SeatNumber = 9, Vacated = false , SeatID = 24},
                                                              new Seat { ScreenID = 1, RowNumber = 2, SeatNumber = 10, Vacated = false , SeatID = 25},
                                                              new Seat { ScreenID = 1, RowNumber = 2, SeatNumber = 11, Vacated = false , SeatID = 26},
                                                              new Seat { ScreenID = 1, RowNumber = 2, SeatNumber = 12, Vacated = false , SeatID = 27},
                                                              new Seat { ScreenID = 1, RowNumber = 2, SeatNumber = 13, Vacated = false , SeatID = 28},
                                                              new Seat { ScreenID = 1, RowNumber = 2, SeatNumber = 14, Vacated = false , SeatID = 29},
                                                              new Seat { ScreenID = 1, RowNumber = 2, SeatNumber = 15, Vacated = false , SeatID = 30},
                                                              new Seat { ScreenID = 1, RowNumber = 3, SeatNumber = 1, Vacated = false , SeatID = 31},
                                                              new Seat { ScreenID = 1, RowNumber = 3, SeatNumber = 2, Vacated = false , SeatID = 32},
                                                              new Seat { ScreenID = 1, RowNumber = 3, SeatNumber = 3, Vacated = false , SeatID = 33},
                                                              new Seat { ScreenID = 1, RowNumber = 3, SeatNumber = 4, Vacated = false , SeatID = 34},
                                                              new Seat { ScreenID = 1, RowNumber = 3, SeatNumber = 5, Vacated = false , SeatID = 35},
                                                              new Seat { ScreenID = 1, RowNumber = 3, SeatNumber = 6, Vacated = false , SeatID = 36},
                                                              new Seat { ScreenID = 1, RowNumber = 3, SeatNumber = 7, Vacated = false , SeatID = 37},
                                                              new Seat { ScreenID = 1, RowNumber = 3, SeatNumber = 8, Vacated = false , SeatID = 38},
                                                              new Seat { ScreenID = 1, RowNumber = 3, SeatNumber = 9, Vacated = false , SeatID = 39},
                                                              new Seat { ScreenID = 1, RowNumber = 3, SeatNumber = 10, Vacated = false , SeatID = 40},
                                                              new Seat { ScreenID = 1, RowNumber = 3, SeatNumber = 11, Vacated = false , SeatID = 41},
                                                              new Seat { ScreenID = 1, RowNumber = 3, SeatNumber = 12, Vacated = false , SeatID = 42},
                                                              new Seat { ScreenID = 1, RowNumber = 3, SeatNumber = 13, Vacated = false , SeatID = 43},
                                                              new Seat { ScreenID = 1, RowNumber = 3, SeatNumber = 14, Vacated = false , SeatID = 44},
                                                              new Seat { ScreenID = 1, RowNumber = 3, SeatNumber = 15, Vacated = false , SeatID = 45} 
                }},
                        CinemaID = 1,
                        LanguageID = 1,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddHours(14).AddMinutes(30),
                        Tickets = new List<Ticket> {
                        new Ticket{ OrderID = 1}, new Ticket{ OrderID = 2},new Ticket{ OrderID = 3},new Ticket{ OrderID = 4},new Ticket{ OrderID = 5},
                        new Ticket{ OrderID = 6}, new Ticket{ OrderID = 7},new Ticket{ OrderID = 8},new Ticket{ OrderID = 9},new Ticket{ OrderID = 10},
                        new Ticket{ OrderID = 11}, new Ticket{ OrderID = 12},new Ticket{ OrderID = 13},new Ticket{ OrderID = 14},new Ticket{ OrderID = 15},

                        new Ticket{ OrderID = 16}, new Ticket{ OrderID = 17},new Ticket{ OrderID = 18},new Ticket{ OrderID = 19},new Ticket{ OrderID = 20},
                        new Ticket{ OrderID = 21}, new Ticket{ OrderID = 22},new Ticket{ OrderID = 23},new Ticket{ OrderID = 24},new Ticket{ OrderID = 25},
                        new Ticket{ OrderID = 26}, new Ticket{ OrderID = 27},new Ticket{ OrderID = 28},new Ticket{ OrderID = 29},new Ticket{ OrderID = 30},

                        new Ticket{ OrderID = 31}, new Ticket{ OrderID = 32},new Ticket{ OrderID = 33},new Ticket{ OrderID = 34},new Ticket{ OrderID = 35},
                        new Ticket{ OrderID = 36}, new Ticket{ OrderID = 37},new Ticket{ OrderID = 38},new Ticket{ OrderID = 39},new Ticket{ OrderID = 40},
                        new Ticket{ OrderID = 41}, new Ticket{ OrderID = 42},new Ticket{ OrderID = 43},new Ticket{ OrderID = 44},new Ticket{ OrderID = 45}
                        }
                    },
        new Show() 
                    {
                        ShowID =1,
                        MovieID = 1,
                        Screen =  new Screen()
            { 
                Size = 45,
                ScreenID = 1,
                Seats = new List<Seat> {                      new Seat { ScreenID = 1, RowNumber = 1, SeatNumber = 1, Vacated = false, SeatID = 1},
                                                              new Seat { ScreenID = 1, RowNumber = 1, SeatNumber = 2, Vacated = false, SeatID = 2},
                                                              new Seat { ScreenID = 1, RowNumber = 1, SeatNumber = 3, Vacated = false, SeatID = 3},
                                                              new Seat { ScreenID = 1, RowNumber = 1, SeatNumber = 4, Vacated = false, SeatID = 4},
                                                              new Seat { ScreenID = 1, RowNumber = 1, SeatNumber = 5, Vacated = false , SeatID = 5},
                                                              new Seat { ScreenID = 1, RowNumber = 1, SeatNumber = 6, Vacated = false , SeatID = 6},
                                                              new Seat { ScreenID = 1, RowNumber = 1, SeatNumber = 7, Vacated = false , SeatID = 7},
                                                              new Seat { ScreenID = 1, RowNumber = 1, SeatNumber = 8, Vacated = false , SeatID = 8},
                                                              new Seat { ScreenID = 1, RowNumber = 1, SeatNumber = 9, Vacated = false , SeatID = 9},
                                                              new Seat { ScreenID = 1, RowNumber = 1, SeatNumber = 10, Vacated = false , SeatID = 10},
                                                              new Seat { ScreenID = 1, RowNumber = 1, SeatNumber = 11, Vacated = false , SeatID = 11},
                                                              new Seat { ScreenID = 1, RowNumber = 1, SeatNumber = 12, Vacated = false , SeatID = 12},
                                                              new Seat { ScreenID = 1, RowNumber = 1, SeatNumber = 13, Vacated = false , SeatID = 13},
                                                              new Seat { ScreenID = 1, RowNumber = 1, SeatNumber = 14, Vacated = false , SeatID = 14},
                                                              new Seat { ScreenID = 1, RowNumber = 1, SeatNumber = 15, Vacated = false , SeatID = 15},
                                                              new Seat { ScreenID = 1, RowNumber = 2, SeatNumber = 1, Vacated = false , SeatID = 16},
                                                              new Seat { ScreenID = 1, RowNumber = 2, SeatNumber = 2, Vacated = false , SeatID = 17},
                                                              new Seat { ScreenID = 1, RowNumber = 2, SeatNumber = 3, Vacated = false , SeatID = 18},
                                                              new Seat { ScreenID = 1, RowNumber = 2, SeatNumber = 4, Vacated = false , SeatID = 19},
                                                              new Seat { ScreenID = 1, RowNumber = 2, SeatNumber = 5, Vacated = false , SeatID = 20},
                                                              new Seat { ScreenID = 1, RowNumber = 2, SeatNumber = 6, Vacated = false , SeatID = 21},
                                                              new Seat { ScreenID = 1, RowNumber = 2, SeatNumber = 7, Vacated = false , SeatID = 22},
                                                              new Seat { ScreenID = 1, RowNumber = 2, SeatNumber = 8, Vacated = false , SeatID = 23},
                                                              new Seat { ScreenID = 1, RowNumber = 2, SeatNumber = 9, Vacated = false , SeatID = 24},
                                                              new Seat { ScreenID = 1, RowNumber = 2, SeatNumber = 10, Vacated = false , SeatID = 25},
                                                              new Seat { ScreenID = 1, RowNumber = 2, SeatNumber = 11, Vacated = false , SeatID = 26},
                                                              new Seat { ScreenID = 1, RowNumber = 2, SeatNumber = 12, Vacated = false , SeatID = 27},
                                                              new Seat { ScreenID = 1, RowNumber = 2, SeatNumber = 13, Vacated = false , SeatID = 28},
                                                              new Seat { ScreenID = 1, RowNumber = 2, SeatNumber = 14, Vacated = false , SeatID = 29},
                                                              new Seat { ScreenID = 1, RowNumber = 2, SeatNumber = 15, Vacated = false , SeatID = 30},
                                                              new Seat { ScreenID = 1, RowNumber = 3, SeatNumber = 1, Vacated = false , SeatID = 31},
                                                              new Seat { ScreenID = 1, RowNumber = 3, SeatNumber = 2, Vacated = false , SeatID = 32},
                                                              new Seat { ScreenID = 1, RowNumber = 3, SeatNumber = 3, Vacated = false , SeatID = 33},
                                                              new Seat { ScreenID = 1, RowNumber = 3, SeatNumber = 4, Vacated = false , SeatID = 34},
                                                              new Seat { ScreenID = 1, RowNumber = 3, SeatNumber = 5, Vacated = false , SeatID = 35},
                                                              new Seat { ScreenID = 1, RowNumber = 3, SeatNumber = 6, Vacated = false , SeatID = 36},
                                                              new Seat { ScreenID = 1, RowNumber = 3, SeatNumber = 7, Vacated = false , SeatID = 37},
                                                              new Seat { ScreenID = 1, RowNumber = 3, SeatNumber = 8, Vacated = false , SeatID = 38},
                                                              new Seat { ScreenID = 1, RowNumber = 3, SeatNumber = 9, Vacated = false , SeatID = 39},
                                                              new Seat { ScreenID = 1, RowNumber = 3, SeatNumber = 10, Vacated = false , SeatID = 40},
                                                              new Seat { ScreenID = 1, RowNumber = 3, SeatNumber = 11, Vacated = false , SeatID = 41},
                                                              new Seat { ScreenID = 1, RowNumber = 3, SeatNumber = 12, Vacated = false , SeatID = 42},
                                                              new Seat { ScreenID = 1, RowNumber = 3, SeatNumber = 13, Vacated = false , SeatID = 43},
                                                              new Seat { ScreenID = 1, RowNumber = 3, SeatNumber = 14, Vacated = false , SeatID = 44},
                                                              new Seat { ScreenID = 1, RowNumber = 3, SeatNumber = 15, Vacated = false , SeatID = 45} 
                }},
                        CinemaID = 1,
                        LanguageID = 1,
                        Subtitles = "Nederlands",
                        Is3D = false,
                        DateTime = DateTime.Today.AddHours(14).AddMinutes(30),
                        Tickets = new List<Ticket> {
                        new Ticket{ OrderID = 1}, new Ticket{ OrderID = 2},new Ticket{ OrderID = 3},new Ticket{ OrderID = 4},new Ticket{ OrderID = 5},
                        new Ticket{ OrderID = 6}, new Ticket{ OrderID = 7},new Ticket{ OrderID = 8},new Ticket{ OrderID = 9},new Ticket{ OrderID = 10},
                        new Ticket{ OrderID = 11}, new Ticket{ OrderID = 12},new Ticket{ OrderID = 13},new Ticket{ OrderID = 14},new Ticket{ OrderID = 15},

                        new Ticket{ OrderID = 16}, new Ticket{ OrderID = 17},new Ticket{ OrderID = 18},new Ticket{ OrderID = 19},new Ticket{ OrderID = 20},
                        new Ticket{ OrderID = 21}, new Ticket{ OrderID = 22},new Ticket{ OrderID = 23},new Ticket{ OrderID = 24},new Ticket{ OrderID = 25},
                        new Ticket{ OrderID = 26}, new Ticket{ OrderID = 27},new Ticket{ OrderID = 28},new Ticket{ OrderID = 29},new Ticket{ OrderID = 30},

                        new Ticket{ OrderID = 31}, new Ticket{ OrderID = 32},new Ticket{ OrderID = 33},new Ticket{ OrderID = 34},new Ticket{ OrderID = 35},
                        new Ticket{ OrderID = 36}, new Ticket{ OrderID = 37},new Ticket{ OrderID = 38},new Ticket{ OrderID = 39},new Ticket{ OrderID = 40},
                        new Ticket{ OrderID = 41}, new Ticket{ OrderID = 42},new Ticket{ OrderID = 43},new Ticket{ OrderID = 44}
                        }
                    }};
        
     

        [TestMethod]
        public void GetMovies_Test()
        {
            //Arrange
            // create some mock products to play with
            ICollection<Movie> movies = new List<Movie>
                {
                    new Movie { Title = "Gooise Vrouwen", Duration = 120, Genres = new List<Genre> { new Genre { Name = "Women" } }, Is3DAvailable = false},
                    new Movie { Title = "50 shades of grey", Duration = 130, Genres = new List<Genre> { new Genre { Name = "Erotic" } }, Is3DAvailable = false },
                    new Movie { Title = "Imitation Game", Duration = 110, Genres = new List<Genre> { new Genre { Name = "Action" } }, Is3DAvailable = true }
                };

            // Mock the Products Repository using Moq
            Mock<ICinemaRepository> mockCinemaRepository = new Mock<ICinemaRepository>();

            // Return all the products
            mockCinemaRepository.Setup(mr => mr.Movies).Returns(movies);


            //Act
            List<Movie> films = mockCinemaRepository.Object.Movies.ToList<Movie>();

            //Assert
            Assert.AreEqual(films.Count(), 3);
        }
        [TestMethod]
        public void GetScreens_Test()
        {
            //Arrange
            // create some mock products to play with
            List<Seat> seats = new List<Seat>{
                new Seat{ RowNumber = 1, ScreenID=2, SeatNumber=1, Vacated=false},
                new Seat{RowNumber = 1, ScreenID=2, SeatNumber=1, Vacated=false}
            };

            IEnumerable<Screen> screens = new List<Screen>
                {
                    new Screen{ScreenID=1, Seats=seats, Size=1},
                    new Screen{ScreenID=2, Seats=seats, Size=300}
                };

            // Mock the Products Repository using Moq
            Mock<ICinemaRepository> mockCinemaRepository = new Mock<ICinemaRepository>();

            // Return all the products
            mockCinemaRepository.Setup(mr => mr.Screens).Returns(screens);


            //Act
            List<Screen> screenies = mockCinemaRepository.Object.Screens.ToList<Screen>();

            //Assert
            Assert.AreEqual(screenies.Count(), 2);
        }

        [TestMethod]
        public void GetSeats_Test()
        {
            //Arrange
            // create some mock products to play with
            List<Seat> seats = new List<Seat>{
                new Seat{ RowNumber = 1, ScreenID=2, SeatNumber=1, Vacated=false},
                new Seat{RowNumber = 1, ScreenID=2, SeatNumber=1, Vacated=false}
            };

            // Mock the Products Repository using Moq
            Mock<ICinemaRepository> mockCinemaRepository = new Mock<ICinemaRepository>();

            // Return all the products
            mockCinemaRepository.Setup(mr => mr.Seats).Returns(seats);


            //Act
            List<Seat> seaties = mockCinemaRepository.Object.Seats.ToList<Seat>();

            //Assert
            Assert.AreEqual(seaties.Count(), 2);
        }

        [TestMethod]
        public void AssignSeats_emptyScreen1SeatRequired()
        {
            //Arrange
           
            // Mock the Products Repository using Moq
            Mock<ICinemaRepository> MockRepo = new Mock<ICinemaRepository>();

            // Return all the items from the repo
            MockRepo.Setup(mr => mr.Shows).Returns(TestShows);
            MockRepo.Setup(x => x.Screens).Returns(TestScreens);
            //MockRepo.Setup(x => x.Shows.ElementAtOrDefault(1).AvailableSeats()).Returns(null);


            //Act
            IEnumerable<Seat> ResultSeats = MockRepo.Object.Shows.First().AssignSeats(1);

            //Assert
            Assert.AreEqual(ResultSeats.First().SeatID, 19);
        }

        [TestMethod]
        public void AssignSeats_emptyScreen2SeatRequired()
        {
            //Arrange

            //Screen
            // Mock the Products Repository using Moq
            Mock<ICinemaRepository> MockRepo = new Mock<ICinemaRepository>();

            // Return all the items from the repo
            MockRepo.Setup(mr => mr.Shows).Returns(TestShows);
            MockRepo.Setup(x => x.Screens).Returns(TestScreens);


            //Act
            IEnumerable<Seat> ResultSeats = MockRepo.Object.Shows.First().AssignSeats(2);

            //Assert
            Assert.AreEqual(ResultSeats.ElementAt(1).SeatID, 20);
            Assert.AreEqual(ResultSeats.ElementAt(0).SeatID, 19);
        }


        [TestMethod]
        public void AssignSeats_emptyScreenMoreThanRowRequired()
        {
            //Arrange

            //Screen
            // Mock the Products Repository using Moq
            Mock<ICinemaRepository> MockRepo = new Mock<ICinemaRepository>();

            // Return all the items from the repo
            MockRepo.Setup(mr => mr.Shows).Returns(TestShows);
            MockRepo.Setup(x => x.Screens).Returns(TestScreens);


            //Act
            IEnumerable<Seat> ResultSeats = MockRepo.Object.Shows.First().AssignSeats(16);

            System.Diagnostics.Debug.WriteLine("//SeatTest 1, seat 1 ID: " + ResultSeats.ElementAt(0).SeatID);
            System.Diagnostics.Debug.WriteLine("//SeatTest 1, Totalseats: " + ResultSeats.Count());

            //Assert
            Assert.AreEqual(ResultSeats.ElementAt(0).SeatID, 16);
            Assert.AreEqual(ResultSeats.ElementAt(15).SeatID, 34);
        }

        [TestMethod]
        public void AssignSeats_Full()
        {
            //Arrange

            //Screen
            // Mock the Products Repository using Moq
            Mock<ICinemaRepository> MockRepo = new Mock<ICinemaRepository>();

            // Return all the items from the repo
            MockRepo.Setup(mr => mr.Shows).Returns(TestShows);


            //Act
            IEnumerable<Seat> ResultSeats = MockRepo.Object.Shows.ElementAt(2).AssignSeats(1);

            //Assert
            Assert.AreEqual(ResultSeats, null);
        }

        [TestMethod]
        public void AssignSeats_1Left()
        {
            //Arrange

            //Screen
            // Mock the Repository using Moq
            Mock<ICinemaRepository> MockRepo = new Mock<ICinemaRepository>();

            // Return all the items from the repo
            MockRepo.Setup(mr => mr.Shows).Returns(TestShows);


            //Act
            IEnumerable<Seat> ResultSeats = MockRepo.Object.Shows.ElementAt(3).AssignSeats(1);

            //Assert
            Assert.AreEqual(ResultSeats.Count(), 1);
        }

        [TestMethod]
        public void AvailableSeatsNoTickets_Test()
        {
            //Arrange

            // Mock the Repository using Moq
            Mock<ICinemaRepository> mockCinemaRepository = new Mock<ICinemaRepository>();

            // Return all the products
            mockCinemaRepository.Setup(mr => mr.Shows).Returns(TestShows);


            //Act
            int result = mockCinemaRepository.Object.Shows.First().AvailableSeats();

            //Assert
            Assert.AreEqual(result, 45);
        }
        [TestMethod]
        public void AvailableSeats1Ticket_Test()
        {
            //Arrange

            // Mock the Repository using Moq
            Mock<ICinemaRepository> mockCinemaRepository = new Mock<ICinemaRepository>();

            // Return all the products
            mockCinemaRepository.Setup(mr => mr.Shows).Returns(TestShows);

            //Act
            int result = mockCinemaRepository.Object.Shows.ElementAt(1).AvailableSeats();

            //Assert
            Assert.AreEqual(44, result);
        }
        [TestMethod]
        public void AvailableSeatsFull_Test()
        {
            //Arrange

            // Mock the Repository using Moq
            Mock<ICinemaRepository> mockCinemaRepository = new Mock<ICinemaRepository>();

            // Return all the products
            mockCinemaRepository.Setup(mr => mr.Shows).Returns(TestShows);

            //Act
            int result = mockCinemaRepository.Object.Shows.ElementAt(2).AvailableSeats();

            //Assert
            Assert.AreEqual(0, result);
        }
    }
}