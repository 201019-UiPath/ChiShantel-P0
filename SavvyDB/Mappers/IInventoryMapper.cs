using System.Collections.Generic;
namespace SavvyDB.Mappers
{
    public interface IInventoryMapper
    {
         SavvyDB.Entities.Inventory ParseInventory(SavvyDB.Models.Inventory Inventory);
         SavvyDB.Models.Inventory ParseInventory(SavvyDB.Entities.Inventory Inventory);
         List<SavvyDB.Models.Inventory> ParseInventory(List<SavvyDB.Entities.Inventory> Inventory);
         List<SavvyDB.Entities.Inventory> ParseInventory(List<SavvyDB.Models.Inventory> Inventory);
         ICollection<SavvyDB.Entities.Inventory> ParseInventory(ICollection<SavvyDB.Models.Inventory> Inventory);
         ICollection<SavvyDB.Models.Inventory> ParseInventory(ICollection<SavvyDB.Entities.Inventory> Inventory);
    }
}