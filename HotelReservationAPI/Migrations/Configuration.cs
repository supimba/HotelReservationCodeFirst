using System.Collections;
using System.Collections.Generic;
using HotelReservationAPI.Models;

namespace HotelReservationAPI.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<HotelReservationAPI.Models.DatabaseContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "HotelReservationAPI.Models.DatabaseContext";
        }

        protected override void Seed(HotelReservationAPI.Models.DatabaseContext context)
        {



           Hotel octodureHotel =new Hotel 
            {
                Name = "Octodure",
                Description = "Idéalement situé à la sortie de l’autoroute, à mi-chemin entre l’Italie et la France, l’hôtel se trouve à quelques minutes en voiture de la Fondation Gianadda.",
                Location = "Martigny",
                Category = 1,
                HasWifi = true,
                HasParking = true,
                Phone = "+41 (0)27 722 71 21",
                Email = "contact@porte-octodure.ch",
                Website = "http://www.porte-octodure.ch/"
           };

            Room singleOctodure1 = new Room()
            {
                Hotel = octodureHotel,
                Number = 1,
                Description = "Nos chambres simples sont toutes non-fumeur.",
                Type = 1,
                Price = 85,
                HasTv = true,
                HasHairDryer = true

            };
            Room singleOctodure2 = new Room()
            {
                Hotel = octodureHotel,
                Number = 2,
                Description = "Cette chambre insonorisée dispose d'une télévision par câble à écran plat et d'une salle de bains. ",
                Type = 1,
                Price = 85,
                HasTv = false,
                HasHairDryer = false

            };
            Room singleOctodure3 = new Room()
            {
                Hotel = octodureHotel,
                Number = 3,
                Description = "Chambre simple style typique de l'octodure.",
                Type = 1,
                Price = 75,
                HasTv = true,
                HasHairDryer = true

            };
            Room singleOctodure4 = new Room()
            {
                Hotel = octodureHotel,
                Number = 4,
                Description = "La chambre la plus calme  de notre hotel. Reposé vous sans TV.",
                Type = 1,
                Price = 85,
                HasTv = false,
                HasHairDryer = true

            };

            Room doubleRoom1 = new Room()
            {
                Hotel = octodureHotel,
                Number = 5,
                Description = "Nos chambres double standard offrent une bonne solution d\'hébergement economique avec tout le confort. ",
                Type = 2,
                Price = 100,
                HasTv = true,
                HasHairDryer = true,
                
            };

            Room doubleRoom2 = new Room()
            {
                Hotel = octodureHotel,
                Number = 6,
                Description = "Chambres calmes et spacieuses, elles offrent une superficie de 25m2. Idéales pour un séjour confortable, elles sont également pourvues d’une grande salle de bain avec baignoire ainsi que d’un dressing. ",
                Type = 2,
                Price = 120,
                HasTv = true,
                HasHairDryer = true
            };

            Room doubleRoom3 = new Room()
            {
                Hotel = octodureHotel,
                Number = 7,
                Description = "Chambre double standard de l'octodure. ",
                Type = 2,
                Price = 90,
                HasTv = true,
                HasHairDryer = true
            };
            Room doubleRoom4 = new Room()
            {
                Hotel = octodureHotel,
                Number = 8,
                Description = "Cette chambre avec balcon est non fumeur.",
                Type = 2,
                Price = 115,
                HasTv = true,
                HasHairDryer = true
            };
            Room doubleRoom5 = new Room()
            {
                Hotel = octodureHotel,
                Number = 9,
                Description = "Parfait pour une nuit en couple. ",
                Type = 2,
                Price = 95,
                HasTv = true,
                HasHairDryer = true
            };

            Picture singleOctodure1Picture = new Picture()
            {
                Room = singleOctodure1,
                Url = "https://www.swisshotels.com/catalog/images/hotels/12333/details/668x370/suite.jpg"

            };
            Picture singleOcto2Picture = new Picture()
            {
                Room = singleOctodure2,
                Url = "https://media-cdn.tripadvisor.com/media/photo-s/02/e8/09/a3/la-porte-d-octodure-hotel.jpg"
            };


            // Add hotel
            context.Hotels.Add(octodureHotel);
            // Add rooms to list
            List<Room> roomsOctoList = new List<Room>();
            roomsOctoList.Add(singleOctodure1);
            roomsOctoList.Add(singleOctodure2);
            roomsOctoList.Add(singleOctodure3);
            roomsOctoList.Add(singleOctodure4);
            roomsOctoList.Add(doubleRoom1);
            roomsOctoList.Add(doubleRoom2);
            roomsOctoList.Add(doubleRoom3);
            roomsOctoList.Add(doubleRoom4);
            roomsOctoList.Add(doubleRoom5);

            context.SaveChanges(); 

            // Add rooms
            context.Rooms.Add(singleOctodure1);
            context.Rooms.Add(singleOctodure2);
            context.Rooms.Add(singleOctodure3);
            context.Rooms.Add(singleOctodure4);

            context.Rooms.Add(doubleRoom1);
            context.Rooms.Add(doubleRoom2);
            context.Rooms.Add(doubleRoom3);
            context.Rooms.Add(doubleRoom4);
            context.Rooms.Add(doubleRoom5);
            context.SaveChanges();

            // Add pictures
            context.Pictures.Add(singleOctodure1Picture);
            context.Pictures.Add(singleOcto2Picture);
            context.SaveChanges();

            octodureHotel.Rooms = roomsOctoList;
            context.SaveChanges();

   

            // Hotel #2 in Martigny 
            Hotel constantinPalace = new Hotel
            {
                Name = "Constantin Palace",
                Description = "Cet hotel est à l'image de son patron, guide, seigneur Constantin. Incontournable.",
                Location = "Martigny",
                Category = 3,
                HasWifi = true,
                HasParking = true,
                Phone = "+41 (0)27 722 60 20",
                Email = "contact@constantin-palace.ch",
                Website = "http://www.christian-constantin.ch/?portfolio=hotel-clerc"
            };

            Room singleConstRoom1 = new Room()
            {
                Hotel = constantinPalace,
                Number = 1,
                Description = "Chambre simple, mais pas si simple. Petite, mais pas si petite. Prix abordable, mais pas bon marché. ",
                Type = 1,
                Price = 150,
                HasTv = false,
                HasHairDryer = false

            };
            Room singleConstRoom2 = new Room()
            {
                Hotel = constantinPalace,
                Number = 2,
                Description = "Venez découvrir la chambre ou les amis politiciens du patron viennent prendre l'apéritif et plus encore. ",
                Type = 1,
                Price = 150,
                HasTv = false,
                HasHairDryer = false

            };

            Room doubleConstRoom1 = new Room()
            {
                Hotel = constantinPalace,
                Number = 3,
                Description = "Doublement plus grande qu une chambre simple. Elle saura vous ravir et vous égayer. ",
                Type = 2,
                Price = 200,
                HasTv = true,
                HasHairDryer = true,

            };

            Room doubleConstRoom2 = new Room()
            {
                Hotel = constantinPalace,
                Number = 4,
                Description = "Cette chambre est spacieuse, formidable comme son patron. ",
                Type = 2,
                Price = 200,
                HasTv = true,
                HasHairDryer = true
            };


            Room doubleConstRoom3 = new Room()
            {
                Hotel = constantinPalace,
                Number = 5,
                Description = "Trois fois plus spacieuse, avec 3 écrans de télévision avec une commande. Elle saura vous inspirez et vous saurez l'aimer. ",
                Type = 2,
                Price = 300,
                HasTv = true,
                HasHairDryer = true
            };

            Room doubleConstRoom4 = new Room()
            {
                Hotel = constantinPalace,
                Number = 6,
                Description = "Quatre fois plus spacieuse, avec 3 baignoires et une grande armoire. Cette chambre est pour les clients exigeants. ",
                Type = 2,
                Price = 400,
                HasTv = true,
                HasHairDryer = true
            };


            Room doubleConstRoom5 = new Room()
            {
                Hotel = constantinPalace,
                Number = 7,
                Description = "Cinq fois plus grande que les plus petites. Un lavabo de luxe, un sèche de cheuveux en bois et une ambiance hors de prix. ",
                Type = 2,
                Price = 500,
                HasTv = true,
                HasHairDryer = true
            };

            Room doubleConstRoom6 = new Room()
            {
                Hotel = constantinPalace,
                Number = 8,
                Description = "Constantin Palace Double Luxe Suite Senior. Cette oeuvre central démontre tout le génie de l'architecture du maître. ",
                Type = 2,
                Price = 1000,
                HasTv = false,
                HasHairDryer = false
            };

            Room doubleConstRoom7 = new Room()
            {
                Hotel = constantinPalace,
                Number = 9,
                Description = "Constantin Palace Double Luxe Suite Senior. Cette oeuvre central démontre tout le génie de l'architecture du maître. ",
                Type = 2,
                Price = 10,
                HasTv = false,
                HasHairDryer = false
            };

            Picture singleConstPicture1 = new Picture()
            {
                Room = singleConstRoom1,
                Url = "https://foto.hrsstatic.com/fotos/0/3/694/390/80/000000/https%3A%2F%2Ffoto-origin.hrsstatic.com%2Ffoto%2F7%2F8%2F1%2F8%2F%2Fteaser_781883.jpg/VCy%2BEVFh%2B2zi2WDiRsqKcw%3D%3D/2300,1533/6/Marconi_Hotel-Rende-Ecomomy_Zimmer_Einzel-781883.jpg"

            };
            Picture singleConstPicture2 = new Picture()
            {
                Room = singleConstRoom2,
                Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTDNvUEohRDzm6w6190qU8Hm0EgSyE8fwDlg_mIFMdlKkVss5acJw"
            };

            Picture doubleConstPicture1= new Picture()
            {
                Room = doubleConstRoom1,
                Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRn7B-pyLHkNZW0MRLcvtm5IWwkM-t-yc74oYYt-7F5IUYtZayi"
            };
            Picture doubleConstPicture2 = new Picture()
            {
                Room = doubleConstRoom2,
                Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSe7ACjLeU4mr4YkmRzK4OcDdDUY7wHv9jE23KOevxTA619MEId"
            };
            Picture doubleConstPicture3= new Picture()
            {
                Room = doubleConstRoom3,
                Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQcCqoYvIHOBXxbl1Ss03igY70jDnrku86uoJV-JrB5v6mQpMVd3A"
            };
            Picture doubleConstPicture4 = new Picture()
            {
                Room = doubleConstRoom4,
                Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRWqLPA1UVXJIfT6pfJGQ69jYK6rwxoGUrFW9xQTsk4bISeRxa-"
            };
            Picture doubleConstPicture5 = new Picture()
            {
                Room = doubleConstRoom5,
                Url = "https://www.hotel-valaisia.ch/fileadmin/media_resources/_processed_/9/9/csm_valaisia_zimmer_viererzimmer_c8df722540.jpg"
            };
            Picture doubleConstPicture6 = new Picture()
            {
                Room = doubleConstRoom6,
                Url = "https://www.berliner-zeitung.de/image/5174222/max/600/450/bedc4f395e763c9e3aa9326d11dd3275/Zk/10-brinker-original2-jpg.jpg"
            };

            Picture doubleConstPicture7 = new Picture()
            {
                Room = doubleConstRoom7,
                Url = "http://9d994bf6ac625bf5bc55-a77ff3994ac009fc59203500350c3ae4.r92.cf1.rackcdn.com/XLGallery/Four-Points-by-Sheraton-Sihlcity-Zurich-Hotel-Superior-Zimmer-King.jpg"
            };


            // Add hotel 
            context.Hotels.Add(constantinPalace);

            // Add rooms to list
            List<Room> constantinRoomsList = new List<Room>();
            constantinRoomsList.Add(singleConstRoom1);
            constantinRoomsList.Add(singleConstRoom2);
            constantinRoomsList.Add(doubleConstRoom1);
            constantinRoomsList.Add(doubleConstRoom2);
            constantinRoomsList.Add(doubleConstRoom3);
            constantinRoomsList.Add(doubleConstRoom4);
            constantinRoomsList.Add(doubleConstRoom5);
            constantinRoomsList.Add(doubleConstRoom6);
            constantinRoomsList.Add(doubleConstRoom7);
            context.SaveChanges();

            // Add rooms
            context.Rooms.Add(singleConstRoom1);
            context.Rooms.Add(singleConstRoom2);
            context.Rooms.Add(doubleConstRoom1);
            context.Rooms.Add(doubleConstRoom2);
            context.Rooms.Add(doubleConstRoom3);
            context.Rooms.Add(doubleConstRoom4);
            context.Rooms.Add(doubleConstRoom5);
            context.Rooms.Add(doubleConstRoom6);
            context.Rooms.Add(doubleConstRoom7);
            context.SaveChanges();

            // Add pictures
            context.Pictures.Add(singleConstPicture1);
            context.Pictures.Add(singleConstPicture2);
            context.Pictures.Add(doubleConstPicture1);
            context.Pictures.Add(doubleConstPicture2);
            context.Pictures.Add(doubleConstPicture3);
            context.Pictures.Add(doubleConstPicture4);
            context.Pictures.Add(doubleConstPicture5);
            context.Pictures.Add(doubleConstPicture6);
            context.Pictures.Add(doubleConstPicture7);
            context.SaveChanges();

            constantinPalace.Rooms = constantinRoomsList;
            context.SaveChanges();
                

            //Hotel #1 Sion
            Hotel valaisPalace = new Hotel
            {
                Name = "Valais Palace",
                Description = "Laissez-vous enchanter par le calme et la nature de Sion. Sion est l’endroit idéal pour oublier, pour quelques jours, le stress quotidien et pour laisser l’âme retrouver la sérénité. Vous appréciez ici l’air pur de la ville tout en jouissant d’un panorama unique. ",
                Location = "Sion",
                Category = 3,
                HasWifi = true,
                HasParking = true,
                Phone = "+41 (0)27 456 10 20",
                Email = "contact@valais-palace.ch",
                Website = "https://www.grand-hotel-du-golf.ch/"
            };

            Room singleVsPalaceRoom1 = new Room()
            {
                Hotel = valaisPalace,
                Number = 1,
                Description = "Chambre simple avec télévision et vue sur la montagne. ",
                Type = 1,
                Price = 50,
                HasTv = true,
                HasHairDryer = true

            };
            Room singleVsPalaceRoom2 = new Room()
            {
                Hotel = valaisPalace,
                Number = 2,
                Description = "Petit chambre lumineuse avec télévision et mini-bar. ",
                Type = 1,
                Price = 50,
                HasTv = true,
                HasHairDryer = true

            };
            Room singleVsPalaceRoom3 = new Room()
            {
                Hotel = valaisPalace,
                Number = 3,
                Description = "Un véritable petit studio avec son lit douillé. ",
                Type = 1,
                Price = 50,
                HasTv = true,
                HasHairDryer = true

            };

            Room doubleVsPalaceRoom1 = new Room()
            {
                Hotel = valaisPalace,
                Number = 4,
                Description = "Grande chambre idéal pour une nuit en couple. ",
                Type = 2,
                Price = 100,
                HasTv = true,
                HasHairDryer = true,

            };

            Room doubleVsPalaceRoom2 = new Room()
            {
                Hotel = valaisPalace,
                Number = 5,
                Description = "Chambre double classique avec vue sur la montagne. ",
                Type = 2,
                Price = 100,
                HasTv = true,
                HasHairDryer = true
            };


            Room doubleVsPalaceRoom3 = new Room()
            {
                Hotel = valaisPalace,
                Number = 6,
                Description = "Cette chambre double à un accès direct à la piscine. ",
                Type = 2,
                Price = 100,
                HasTv = true,
                HasHairDryer = true
            };

            Room doubleVsPalaceRoom4 = new Room()
            {
                Hotel = valaisPalace,
                Number = 7,
                Description = "Double lit avec écran géant.",
                Type = 2,
                Price = 100,
                HasTv = true,
                HasHairDryer = true
            };


            Room doubleVsPalaceRoom5 = new Room()
            {
                Hotel = valaisPalace,
                Number = 8,
                Description = "Grand espace pour deux personnes. ",
                Type = 2,
                Price = 100,
                HasTv = true,
                HasHairDryer = true
            };

            Room doubleVsPalaceRoom6 = new Room()
            {
                Hotel = valaisPalace,
                Number = 9,
                Description = "Un lit deux places de luxe avec accès au sauna. ",
                Type = 2,
                Price = 100,
                HasTv = true,
                HasHairDryer = false
            };

            Room doubleVsPalaceRoom7 = new Room()
            {
                Hotel = valaisPalace,
                Number = 10,
                Description = "Grande chambre deux places lumineuse. ",
                Type = 2,
                Price = 100,
                HasTv = true,
                HasHairDryer = true
            };

            Room doubleVsPalaceRoom8 = new Room()
            {
                Hotel = valaisPalace,
                Number = 11,
                Description = "Junior suite pour une nuit dans les étoiles. ",
                Type = 2,
                Price = 200,
                HasTv = true,
                HasHairDryer = false
            };

            Picture singleVsPicture1 = new Picture()
            {
                Room = singleVsPalaceRoom1,
                Url = "https://foto.hrsstatic.com/fotos/0/3/694/390/80/000000/https%3A%2F%2Ffoto-origin.hrsstatic.com%2Ffoto%2F7%2F8%2F1%2F8%2F%2Fteaser_781883.jpg/VCy%2BEVFh%2B2zi2WDiRsqKcw%3D%3D/2300,1533/6/Marconi_Hotel-Rende-Ecomomy_Zimmer_Einzel-781883.jpg"

            };
            Picture singleVsPicture2 = new Picture()
            {
                Room = singleVsPalaceRoom2,
                Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTDNvUEohRDzm6w6190qU8Hm0EgSyE8fwDlg_mIFMdlKkVss5acJw"
            };

            Picture singleVsPicture3 = new Picture()
            {
                Room = singleVsPalaceRoom3,
                Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTDNvUEohRDzm6w6190qU8Hm0EgSyE8fwDlg_mIFMdlKkVss5acJw"
            };

            Picture doubleVsPicture1 = new Picture()
            {
                Room = doubleVsPalaceRoom1,
                Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRn7B-pyLHkNZW0MRLcvtm5IWwkM-t-yc74oYYt-7F5IUYtZayi"
            };
            Picture doubleVsPicture2 = new Picture()
            {
                Room = doubleVsPalaceRoom2,
                Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSe7ACjLeU4mr4YkmRzK4OcDdDUY7wHv9jE23KOevxTA619MEId"
            };
            Picture doubleVsPicture3 = new Picture()
            {
                Room = doubleVsPalaceRoom3,
                Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQcCqoYvIHOBXxbl1Ss03igY70jDnrku86uoJV-JrB5v6mQpMVd3A"
            };
            Picture doubleVsPicture4 = new Picture()
            {
                Room = doubleVsPalaceRoom4,
                Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRWqLPA1UVXJIfT6pfJGQ69jYK6rwxoGUrFW9xQTsk4bISeRxa-"
            };
            Picture doubleVsPicture5 = new Picture()
            {
                Room = doubleVsPalaceRoom5,
                Url = "https://www.hotel-valaisia.ch/fileadmin/media_resources/_processed_/9/9/csm_valaisia_zimmer_viererzimmer_c8df722540.jpg"
            };
            Picture doubleVsPicture6 = new Picture()
            {
                Room = doubleVsPalaceRoom6,
                Url = "https://www.berliner-zeitung.de/image/5174222/max/600/450/bedc4f395e763c9e3aa9326d11dd3275/Zk/10-brinker-original2-jpg.jpg"
            };

            Picture doubleVsPicture7 = new Picture()
            {
                Room = doubleVsPalaceRoom7,
                Url = "http://9d994bf6ac625bf5bc55-a77ff3994ac009fc59203500350c3ae4.r92.cf1.rackcdn.com/XLGallery/Four-Points-by-Sheraton-Sihlcity-Zurich-Hotel-Superior-Zimmer-King.jpg"
            };
            Picture doubleVsPicture8 = new Picture()
            {
                Room = doubleVsPalaceRoom8,
                Url = "http://9d994bf6ac625bf5bc55-a77ff3994ac009fc59203500350c3ae4.r92.cf1.rackcdn.com/XLGallery/Four-Points-by-Sheraton-Sihlcity-Zurich-Hotel-Superior-Zimmer-King.jpg"
            };

            // Add hotel 
            context.Hotels.Add(valaisPalace);

            // Add rooms to list
            List<Room> valaisPalaceRoomsList = new List<Room>();
            valaisPalaceRoomsList.Add(singleVsPalaceRoom1);
            valaisPalaceRoomsList.Add(singleVsPalaceRoom2);
            valaisPalaceRoomsList.Add(singleVsPalaceRoom3);
            valaisPalaceRoomsList.Add(doubleVsPalaceRoom1);
            valaisPalaceRoomsList.Add(doubleVsPalaceRoom2);
            valaisPalaceRoomsList.Add(doubleVsPalaceRoom3);
            valaisPalaceRoomsList.Add(doubleVsPalaceRoom4);
            valaisPalaceRoomsList.Add(doubleVsPalaceRoom5);
            valaisPalaceRoomsList.Add(doubleVsPalaceRoom6);
            valaisPalaceRoomsList.Add(doubleVsPalaceRoom7);
            valaisPalaceRoomsList.Add(doubleVsPalaceRoom8);

            context.SaveChanges();

            // Add rooms
            context.Rooms.Add(singleVsPalaceRoom1);
            context.Rooms.Add(singleVsPalaceRoom2);
            context.Rooms.Add(singleVsPalaceRoom3);
            context.Rooms.Add(doubleVsPalaceRoom1);
            context.Rooms.Add(doubleVsPalaceRoom2);
            context.Rooms.Add(doubleVsPalaceRoom3);
            context.Rooms.Add(doubleVsPalaceRoom4);
            context.Rooms.Add(doubleVsPalaceRoom5);
            context.Rooms.Add(doubleVsPalaceRoom6);
            context.Rooms.Add(doubleVsPalaceRoom7);
            context.Rooms.Add(doubleVsPalaceRoom8);
            context.SaveChanges();

            // Add pictures
            context.Pictures.Add(singleVsPicture1);
            context.Pictures.Add(singleVsPicture2);
            context.Pictures.Add(singleVsPicture3);
            context.Pictures.Add(doubleVsPicture1);
            context.Pictures.Add(doubleVsPicture2);
            context.Pictures.Add(doubleVsPicture3);
            context.Pictures.Add(doubleVsPicture4);
            context.Pictures.Add(doubleVsPicture5);
            context.Pictures.Add(doubleVsPicture6);
            context.Pictures.Add(doubleVsPicture7);
            context.Pictures.Add(doubleVsPicture8);
            context.SaveChanges();

            valaisPalace.Rooms = valaisPalaceRoomsList;
            context.SaveChanges();



            //Hotel #2 Sion
            Hotel GrandDuc = new Hotel
            {
                Name = "Grand Duc",
                Description = "Installé à seulement vingt minutes à pied de la Gare de Sion, l'hôtel fournit un endroit pratique à ses clients lors de leur visite des châteaux. Il met à disposition des salles de réunion, une consigne à bagage et une réception ouverte 24h/24. ",
                Location = "Sion",
                Category = 3,
                HasWifi = true,
                HasParking = true,
                Phone = "+41 (0)27 456 27 27",
                Email = "contact@granduc.ch",
                Website = "http://www.christian-constantin.ch/?portfolio=hotel-clerc"
            };

            Room singleGrandDucRoom1 = new Room()
            {
                Hotel = GrandDuc,
                Number = 1,
                Description = "Les chambres simples sont composées d'un lit de 100*200 et idéales pour les séjours entre 1 et 3 nuits. ",
                Type = 1,
                Price = 50,
                HasTv = true,
                HasHairDryer = true

            };
            Room singleGrandDucRoom2 = new Room()
            {
                Hotel = GrandDuc,
                Number = 2,
                Description = "Les chambres simples sont composées d'un lit de 100*200 et idéales pour les séjours entre 1 et 3 nuits. ",
                Type = 1,
                Price = 50,
                HasTv = true,
                HasHairDryer = true

            };
            Room singleGrandDucRoom3 = new Room()
            {
                Hotel = GrandDuc,
                Number = 3,
                Description = "Les chambres simples sont composées d'un lit de 100*200 et idéales pour les séjours entre 1 et 3 nuits. ",
                Type = 1,
                Price = 50,
                HasTv = true,
                HasHairDryer = true

            };
            Room singleGrandDucRoom4 = new Room()
            {
                Hotel = GrandDuc,
                Number = 4,
                Description = "Les chambres simples sont composées d'un lit de 100*200 et idéales pour les séjours entre 1 et 3 nuits. ",
                Type = 1,
                Price = 50,
                HasTv = true,
                HasHairDryer = true

            };

            Room doubleGrandDucRoom1 = new Room()
            {
                Hotel = GrandDuc,
                Number = 5,
                Description = "Les chambres doubles sont composées de 2 lits jumeaux côte à côte et séparables de 90*200 et idéales pour  tous les séjours de tourismes ou d'affaire. Ces chambres peuvent également être loués en occupation individuelle pour 1 personne. ",
                Type = 2,
                Price = 100,
                HasTv = true,
                HasHairDryer = true,

            };

            Room doubleGrandDucRoom2 = new Room()
            {
                Hotel = GrandDuc,
                Number = 6,
                Description = "Les chambres doubles sont composées de 2 lits jumeaux côte à côte et séparables de 90*200 et idéales pour  tous les séjours de tourismes ou d'affaire. Ces chambres peuvent également être loués en occupation individuelle pour 1 personne. ",
                Type = 2,
                Price = 100,
                HasTv = true,
                HasHairDryer = true,

            };


            Picture singleGranDucPicture1 = new Picture()
            {
                Room = singleGrandDucRoom1,
                Url = "https://foto.hrsstatic.com/fotos/0/3/694/390/80/000000/https%3A%2F%2Ffoto-origin.hrsstatic.com%2Ffoto%2F7%2F8%2F1%2F8%2F%2Fteaser_781883.jpg/VCy%2BEVFh%2B2zi2WDiRsqKcw%3D%3D/2300,1533/6/Marconi_Hotel-Rende-Ecomomy_Zimmer_Einzel-781883.jpg"

            };
            Picture singleGranDucPicture2 = new Picture()
            {
                Room = singleGrandDucRoom2,
                Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTDNvUEohRDzm6w6190qU8Hm0EgSyE8fwDlg_mIFMdlKkVss5acJw"
            };

            Picture singleGranDucPicture3 = new Picture()
            {
                Room = singleGrandDucRoom3,
                Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTDNvUEohRDzm6w6190qU8Hm0EgSyE8fwDlg_mIFMdlKkVss5acJw"
            };

            Picture singleGranDucPicture4 = new Picture()
            {
                Room = singleGrandDucRoom4,
                Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRn7B-pyLHkNZW0MRLcvtm5IWwkM-t-yc74oYYt-7F5IUYtZayi"
            };
            Picture doubleGrandDUcPicture1 = new Picture()
            {
                Room = doubleGrandDucRoom1,
                Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSe7ACjLeU4mr4YkmRzK4OcDdDUY7wHv9jE23KOevxTA619MEId"
            };
            Picture doubleGrandDUcPicture2 = new Picture()
            {
                Room = doubleGrandDucRoom2,
                Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQcCqoYvIHOBXxbl1Ss03igY70jDnrku86uoJV-JrB5v6mQpMVd3A"
            };


            // Add hotel 
            context.Hotels.Add(GrandDuc);

            // Add rooms to list
            List<Room> grandDucRoomsList = new List<Room>();
            grandDucRoomsList.Add(singleGrandDucRoom1);
            grandDucRoomsList.Add(singleGrandDucRoom2);
            grandDucRoomsList.Add(singleGrandDucRoom3);
            grandDucRoomsList.Add(singleGrandDucRoom4);
            grandDucRoomsList.Add(doubleGrandDucRoom1);
            grandDucRoomsList.Add(doubleGrandDucRoom2);
    
            context.SaveChanges();

            // Add rooms
            context.Rooms.Add(singleGrandDucRoom1);
            context.Rooms.Add(singleGrandDucRoom2);
            context.Rooms.Add(singleGrandDucRoom3);
            context.Rooms.Add(singleGrandDucRoom4);
            context.Rooms.Add(doubleGrandDucRoom1);
            context.Rooms.Add(doubleGrandDucRoom2);

            context.SaveChanges();

            // Add pictures
            context.Pictures.Add(singleGranDucPicture1);
            context.Pictures.Add(singleGranDucPicture2);
            context.Pictures.Add(singleGranDucPicture3);
            context.Pictures.Add(singleGranDucPicture4);
            context.Pictures.Add(doubleGrandDUcPicture1);
            context.Pictures.Add(doubleGrandDUcPicture2);
           
            context.SaveChanges();

            GrandDuc.Rooms = grandDucRoomsList;
            context.SaveChanges();


            //Hotel #1 Brig

            Hotel walliserPalace = new Hotel
            {
                Name = "Walliser Palace",
                Description = "Das 3-Sterne-Hotel Garni Europe ist ein moderner Familienbetrieb mit neu renovierten Zimmern, einer gemütlichen Hotelbar und hoteleigenen Gratisparkplätzen. Direkt gegenüber der Matterhorn-Gotthard-Bahn und dem Glacier Express erwartet Sie das Hotel Europe. Sie wohnen 200 m vom Zentrum des Ortes Brig entfernt. Kostenfrei profitieren Sie von dem Parkmöglichkeiten vor Ort und vom WLAN. ",
                Location = "Brig",
                Category = 3,
                HasWifi = true,
                HasParking = true,
                Phone = "+41 (0)27 923 13 21",
                Email = "contact@wallisser-palace.ch",
                Website = "http://www.hotel-europe-brig.ch/?skd-language-code=de"
            };

            Room singleWalliserRoom1 = new Room()
            {
                Hotel = walliserPalace,
                Number = 1,
                Description = "Die Zimmer des Hotels Unterellmau bieten herrliche Ausblicke in die Bergwelt rund um Brig Hinterglemm. ",
                Type = 1,
                Price = 65,
                HasTv = true,
                HasHairDryer = true

            };
            Room singleWalliserRoom2 = new Room()
            {
                Hotel = walliserPalace,
                Number = 2,
                Description = "Diese individuell eingerichteten Zimmer bieten Ihnen den optimalen Komfort für einen gelungenen Kurzaufenthalt. Sei es für den Geschäftstrip oder das traute Wochenende zu zweit. ",
                Type = 1,
                Price = 70,
                HasTv = true,
                HasHairDryer = true

            };
            Room singleWalliserRoom3 = new Room()
            {
                Hotel = walliserPalace,
                Number = 3,
                Description = "Klein, aber fein: Unsere beiden Classic-Einzelzimmer bieten Ihnen alles, was es für einen gelungenen Aufenthalt braucht. Schön kompakt zu fairen Preisen. ",
                Type = 1,
                Price = 60,
                HasTv = true,
                HasHairDryer = true

            };
            Room singleWalliserRoom4 = new Room()
            {
                Hotel = walliserPalace,
                Number = 4,
                Description = "Diese individuell eingerichteten Zimmer bieten Ihnen den optimalen Komfort für einen gelungenen Kurzaufenthalt. Sei es für den Geschäftstrip oder das traute Wochenende zu zweit. ",
                Type = 1,
                Price = 50,
                HasTv = true,
                HasHairDryer = true

            };

            Room doubleWalisserRoom1 = new Room()
            {
                Hotel = walliserPalace,
                Number = 5,
                Description = "Historisch angehauchter Charme für wenig Geld. Die originellen Retro-Zimmer mit dem externen Bad über den Flur versprühen einen Hauch von Geschichte. ",
                Type = 2,
                Price = 100,
                HasTv = true,
                HasHairDryer = true,

            };

            Room doubleWalisserRoom2 = new Room()
            {
                Hotel = walliserPalace,
                Number = 6,
                Description = "Luxuriöses und elegantes Zimmer mit Hydromassagedusche im Bad. ",
                Type = 2,
                Price = 200,
                HasTv = true,
                HasHairDryer = true,

            };

            Room doubleWalisserRoom3= new Room()
            {
                Hotel = walliserPalace,
                Number = 7,
                Description = "Lässt Herzen höher schlagen: Wer unsere wunderbare Suite mit Wohn- und Schlafbereich erst einmal bezogen hat, will so schnell nicht mehr heraus. Für Geniesser, die gern mit offenen Augen träumen. ",
                Type = 2,
                Price = 125,
                HasTv = true,
                HasHairDryer = true,

            };


            Picture singleWalisserPicture1 = new Picture()
            {
                Room = singleWalliserRoom1,
                Url = "https://www.auberge-langenthal.ch/images/content/zimmer/Einzelzimmer_Retro_2_Hotel_Auberge_Langenthal.jpg"

            };
            Picture singleWalisserPicture2 = new Picture()
            {
                Room = singleWalliserRoom2,
                Url = "https://www.auberge-langenthal.ch/images/content/zimmer/Doppelzimmer_Classic_Grandlit_Hotel_Auberge_Langenthal.jpg"
            };

            Picture singleWalisserPicture3 = new Picture()
            {
                Room = singleWalliserRoom3,
                Url = "https://www.auberge-langenthal.ch/images/content/zimmer/Doppelzimmer_Superior_Bad_2_Hotel_Auberge_Langenthal.jpg"
            };

            Picture singleWalisserPicture4 = new Picture()
            {
                Room = singleWalliserRoom4,
                Url = "https://www.auberge-langenthal.ch/images/content/zimmer/Einzelzimmer_Retro_Hotel_Auberge_Langenthal.jpg"
            };
            Picture doubleWalisserPicture1 = new Picture()
            {
                Room = doubleWalisserRoom1,
                Url = "https://www.auberge-langenthal.ch/images/content/zimmer/Doppelzimmer_Retro_6_Hotel_Auberge_Langenthal.jpg"
            };
            Picture doubleWalisserPicture2 = new Picture()
            {
                Room = doubleWalisserRoom2,
                Url = "https://www.auberge-langenthal.ch/images/content/zimmer/Doppelzimmer_Retro_5_Hotel_Auberge_Langenthal.jpg"
            };

            Picture doubleWalisserPicture3 = new Picture()
            {
                Room = doubleWalisserRoom1,
                Url = "https://www.auberge-langenthal.ch/images/content/zimmer/Doppelzimmer_Superior_3_Hotel_Auberge_Langenthal.jpg"
            };
            // URLs
            //https://www.auberge-langenthal.ch/images/content/zimmer/Einzelzimmer_Classic_13_Hotel_Auberge_Langenthal.jpg

            // Add hotel 
            context.Hotels.Add(walliserPalace);

            // Add rooms to list
            List<Room> wallisserRoomsList = new List<Room>();
            wallisserRoomsList.Add(singleWalliserRoom1);
            wallisserRoomsList.Add(singleWalliserRoom2);
            wallisserRoomsList.Add(singleWalliserRoom3);
            wallisserRoomsList.Add(singleWalliserRoom4);
            wallisserRoomsList.Add(doubleWalisserRoom1);
            wallisserRoomsList.Add(doubleWalisserRoom2);
            wallisserRoomsList.Add(doubleWalisserRoom3);

            context.SaveChanges();

            // Add rooms
            context.Rooms.Add(singleWalliserRoom1);
            context.Rooms.Add(singleWalliserRoom2);
            context.Rooms.Add(singleWalliserRoom3);
            context.Rooms.Add(singleWalliserRoom4);
            context.Rooms.Add(doubleWalisserRoom1);
            context.Rooms.Add(doubleWalisserRoom2);
            context.Rooms.Add(doubleWalisserRoom3);

            context.SaveChanges();

            // Add pictures
            context.Pictures.Add(singleWalisserPicture1);
            context.Pictures.Add(singleWalisserPicture2);
            context.Pictures.Add(singleWalisserPicture3);
            context.Pictures.Add(singleWalisserPicture4);
            context.Pictures.Add(doubleWalisserPicture1);
            context.Pictures.Add(doubleWalisserPicture2);
            context.Pictures.Add(doubleWalisserPicture3);

            context.SaveChanges();

            walliserPalace.Rooms = wallisserRoomsList;
            context.SaveChanges();




            //Hotel #2 Brig
            Hotel mattherhorn = new Hotel
            {
                Name = "Mattherhorn",
                Description = "Grandiose Suite Matterhorn mit Balkon und Blick auf die atemberaubende Bergwelt. ",
                Location = "Brig",
                Category = 4,
                HasWifi = true,
                HasParking = true,
                Phone = "+41 (0)27 923 08 88",
                Email = "contact@mattherhorn-hotel.ch",
                Website = "https://www.matterhorn-focus.ch/de/"
            };

            Room singleMattherRoom1 = new Room()
            {
                Hotel = mattherhorn,
                Number = 1,
                Description = "Geniessen Sie alpin-modernes Design und die fantastische Aussicht auf das Matterhorn. ",
                Type = 1,
                Price = 75,
                HasTv = true,
                HasHairDryer = true

            };
            Room singleMattherRoom2 = new Room()
            {
                Hotel = mattherhorn,
                Number = 2,
                Description = "Den Sonnenaufgang und den Sonnenuntergang am Matterhorn beobachten. ",
                Type = 1,
                Price = 80,
                HasTv = true,
                HasHairDryer = true

            };
            Room singleMattherRoom3 = new Room()
            {
                Hotel = mattherhorn,
                Number = 3,
                Description = "Naturschauspiel bietet sich Ihnen in Ihren komfortablen Zimmer mit Südbalkon. ",
                Type = 1,
                Price = 85,
                HasTv = true,
                HasHairDryer = true

            };
            Room singleMattherRoom4 = new Room()
            {
                Hotel = mattherhorn,
                Number = 4,
                Description = "Lassen Sie sich in einen Sessel sinken und finden Sie Erholung im traditionellen Ambiente mit warmen Farben und viel Holz. ",
                Type = 1,
                Price = 90,
                HasTv = true,
                HasHairDryer = true

            };
            Room singleMattherRoom5 = new Room()
            {
                Hotel = mattherhorn,
                Number = 5,
                Description = "Wahrer Luxus ist Raum – im Doppelzimmer Design zur Alleinbenützung.  ",
                Type = 1,
                Price = 120,
                HasTv = true,
                HasHairDryer = true

            };

            Room doubleWMatherRoom1 = new Room()
            {
                Hotel = mattherhorn,
                Number = 6,
                Description = "Erleben Sie alpin-modernes Design in Perfektion – eine Harmonie aus Stein, Glas und Holz.   ",
                Type = 2,
                Price = 140,
                HasTv = true,
                HasHairDryer = true,

            };

            Room doubleWMatherRoom2 = new Room()
            {
                Hotel = mattherhorn,
                Number = 7,
                Description = "Luxuriöses und elegantes Zimmer mit Hydromassagedusche im Bad. ",
                Type = 2,
                Price = 200,
                HasTv = true,
                HasHairDryer = true,

            };


            Picture singleMatherPicture1 = new Picture()
            {
                Room = singleMattherRoom1,
                Url = "http://www.eden-spiez.ch/media/img/galleries/komfort-dorf-zimmer/weblication/wThumbnails/Hotel-Eden-Spiez-MOD1QM-315-02-aabe28d66a1e383g5649483053f7d1a9.jpg"

            };
            Picture singleMatherPicture2 = new Picture()
            {
                Room = singleMattherRoom2,
                Url = "https://europe-zermatt.ch/wp-content/uploads/2016/04/Europe_Zermatt_Doppelzimmer_Design_BadZi.jpg"
            };

            Picture singleMatherPicture3 = new Picture()
            {
                Room = singleMattherRoom3,
                Url = "http://www.eden-spiez.ch/media/img/galleries/komfort-dorf-zimmer/weblication/wThumbnails/Hotel-Eden-Spiez-MOD1QM-116-01-c72f00dc1bf15e7g9950b6c52f2785e7.jpg"
            };

            Picture singleMatherPicture4 = new Picture()
            {
                Room = singleMattherRoom4,
                Url = "https://europe-zermatt.ch/wp-content/uploads/2016/04/Europe_Zermatt_Doppelzimmer_Matterhorn_Bad.jpg"
            };
            Picture singleMatherPicture5 = new Picture()
            {
                Room = singleMattherRoom5,
                Url = "https://europe-zermatt.ch/wp-content/uploads/2016/04/Europe_Zermatt_Doppelzimmer_Matterhorn_View.jpg"
            };
            Picture doubleMatherPicture1 = new Picture()
            {
                Room = doubleWMatherRoom1,
                Url = "https://europe-zermatt.ch/wp-content/uploads/2016/04/Europe_Zermatt_Doppelzimmer_Design.jpg"
            };

            Picture doubleMatherPicture2 = new Picture()
            {
                Room = doubleWMatherRoom2,
                Url = "https://europe-zermatt.ch/wp-content/uploads/2016/04/Europe_Zermatt_Doppelzimmer_Standard.jpg"
            };

            //Komfortables Doppelzimmer mit Blick auf die atemberaubende Bergwelt.

            // Add hotel 
            context.Hotels.Add(mattherhorn);

            // Add rooms to list
            List<Room> matterhornRoomsList = new List<Room>();
            matterhornRoomsList.Add(singleMattherRoom1);
            matterhornRoomsList.Add(singleMattherRoom2);
            matterhornRoomsList.Add(singleMattherRoom3);
            matterhornRoomsList.Add(singleMattherRoom4);
            matterhornRoomsList.Add(singleMattherRoom5);
            matterhornRoomsList.Add(doubleWMatherRoom1);
            matterhornRoomsList.Add(doubleWMatherRoom2);


            context.SaveChanges();

            // Add rooms
            context.Rooms.Add(singleMattherRoom1);
            context.Rooms.Add(singleMattherRoom2);
            context.Rooms.Add(singleMattherRoom3);
            context.Rooms.Add(singleMattherRoom4);
            context.Rooms.Add(singleMattherRoom5);
            context.Rooms.Add(doubleWMatherRoom1);
            context.Rooms.Add(doubleWMatherRoom2);

            context.SaveChanges();

            // Add pictures
            context.Pictures.Add(singleMatherPicture1);
            context.Pictures.Add(singleMatherPicture2);
            context.Pictures.Add(singleMatherPicture3);
            context.Pictures.Add(singleMatherPicture4);
            context.Pictures.Add(singleMatherPicture5);
            context.Pictures.Add(doubleMatherPicture1);
            context.Pictures.Add(doubleMatherPicture2);

            context.SaveChanges();

            mattherhorn.Rooms = matterhornRoomsList;
            context.SaveChanges();
            
        }
    }
}
