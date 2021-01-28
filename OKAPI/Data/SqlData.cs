using OKAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OKAPI.Data
{

    public class SqlData : IData
    {
        private Context _context;
        public SqlData(Context context)
        {
            _context = context;
        }

        public Slot AddSlot(Slot slot)
        {
            slot.Id = Guid.NewGuid();
            _context.Slots.Add(slot);
            _context.SaveChanges();
            return slot;
        }

        public void DeleteSlot(Slot slot)
        {
            _context.Slots.Remove(slot);
            _context.SaveChanges();
        }

        public Slot EditSlot(Slot slot)
        {
            var existingSlot = _context.Slots.Find(slot.Id);
            if (existingSlot != null) 
            {
                existingSlot.Status = slot.Status;
                _context.Slots.Update(existingSlot);
                _context.SaveChanges();
            }
            return slot;
        }

        public Slot GetSlot(Guid id)
        {
            var slot = _context.Slots.Find(id);

            return slot;
        }

        public List<Slot> GetSlots()
        {
            return _context.Slots.ToList();
        }
    }
}
