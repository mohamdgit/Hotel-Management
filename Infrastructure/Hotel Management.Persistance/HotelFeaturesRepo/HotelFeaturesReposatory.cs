using AutoMapper.Features;
using Hotel_Management.DOMAIN.Contexts.HotelContext;
using Hotel_Management.DOMAIN.Contracts.HotelFeatureRepo;
using Hotel_Management.DOMAIN.Models.HotelModel;
using Hotel_Management.ServiceImplementiton.Services.HotelService;

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management.Persistance.HotelFeaturesRepo
{
    public class HotelFeaturesReposatory : IHotelFeatureReposatory
    {
        private readonly HotelContext _context;

        public HotelFeaturesReposatory(HotelContext Context)
        {
            _context = Context;
        }
       

       
        public async Task AddHotelFeaturesAsync(IEnumerable<int> ids, int hotelId)
        {
            foreach (var feature in ids)
            {
                var exists = await _context.HotelFeatures
                    .AnyAsync(hf => hf.HotelId == hotelId && hf.FeatureId == feature);

                if (!exists)
                {
                    _context.HotelFeatures.Add(new DOMAIN.Models.HotelModel.HotelFeatures
                    {
                        HotelId = hotelId,
                        FeatureId = feature
                    });
                }
            }
        }

        public async Task Delete(int featureid, int hotelId)
        {
            var existfeatures = await _context.HotelFeatures
                               .FirstOrDefaultAsync(p =>p.FeatureId== featureid && p.HotelId==hotelId);

          
            if (existfeatures is not null)
            {
                     _context.HotelFeatures
                              .Remove(existfeatures);
            }
        }

        public async Task<IEnumerable<DOMAIN.Models.HotelModel.HotelFeatures>> GetAllHotelFeaturesAsync(int hotelId)
        {
            var existfeatures =  _context.HotelFeatures
                                           .Where(p => p.HotelId == hotelId)
                                           .Include(p=>p.Feature).Include(hf => hf.HotelOfFeatures);
            return await existfeatures.ToListAsync(); ;
        }

      
    }
}
