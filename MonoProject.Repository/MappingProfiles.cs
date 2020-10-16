using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MonoProject.Model;
using MonoProject.DAL;

using AutoMapper;

namespace MonoProject.Repository
{
    public class MappingProfiles : Profile
	{
		public MappingProfiles()
		{
			CreateMap<GameInfo,				  GameInfoEntity			    >().ReverseMap();
			CreateMap<GenreTag,		  GenreTagEntity		>().ReverseMap();
			CreateMap<GameInfoGenreTag, GameInfoGenreTagEntity>().ReverseMap();
		}
	}
}