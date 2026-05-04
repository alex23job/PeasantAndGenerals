using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReceptSet : MonoBehaviour
{
    private List<SimpleRecept> recepts = new List<SimpleRecept>();

    // Start is called before the first frame update
    void Start()
    {
        recepts.Clear();
        recepts.Add(new SimpleRecept("Картошка с грибами", 5, 1, new Ingridient[] { new Ingridient("Картофель", 1, 1), new Ingridient("Гриб", 2, 1) }));
        recepts.Add(new SimpleRecept("Картофель отварной", 3, 1, new Ingridient[] { new Ingridient("Картофель", 1, 1) }));
        recepts.Add(new SimpleRecept("Жаркое с тетеревом", 7, 1, new Ingridient[] { new Ingridient("Картофель", 1, 1), new Ingridient("Тетерев", 3, 1) }));
        recepts.Add(new SimpleRecept("Тетерев жаренный", 5, 2, new Ingridient[] { new Ingridient("Тетерев", 3, 1) }));
        recepts.Add(new SimpleRecept("Зелёные щи", 5, 1, new Ingridient[] { new Ingridient("Картофель", 1, 1), new Ingridient("Щавель", 4, 1), new Ingridient("Тетерев", 3, 1) }));
        recepts.Add(new SimpleRecept("Компот", 2, 1, new Ingridient[] { new Ingridient("Яблоко", 5, 1), new Ingridient("Слива", 6, 1) }));
        recepts.Add(new SimpleRecept("Салат", 2, 0, new Ingridient[] { new Ingridient("Огурец", 7, 1), new Ingridient("Щавель", 4, 1) }));
        recepts.Add(new SimpleRecept("Фрукты", 2, 0, new Ingridient[] { new Ingridient("Яблоко", 5, 1), new Ingridient("Слива", 6, 1) }));
        recepts.Add(new SimpleRecept("Рыба жаренная", 4, 2, new Ingridient[] { new Ingridient("Рыба", 8, 1) }));
        recepts.Add(new SimpleRecept("Уха", 6, 1, new Ingridient[] { new Ingridient("Картофель", 1, 1), new Ingridient("Рыба", 8, 1) }));
    }

    public SimpleRecept[] GetRecepts()
    {
        return recepts.ToArray();
    }

}

public class SimpleRecept
{
    public string Name;
    public int PercentVita;
    public int Mode;
    public Ingridient[] Sostav;

    public SimpleRecept() { }
    public SimpleRecept(string name, int percentVita, int md, Ingridient[] ingrs)
    {
        Name = name;
        PercentVita = percentVita;
        Mode = md;
        Sostav = ingrs;
    }

    public bool CheeckRecept(Inventory inv)
    {
        foreach(Ingridient ingr in Sostav)
        {
            if (inv.CheckItem(ingr.ID, ingr.Count)) continue;
            else return false;
        }
        return true;
    }
}

public class Ingridient
{
    public string Name;
    public int ID;
    public int Count;

    public Ingridient() { }
    public Ingridient(string name, int id, int count)
    {
        Name = name;
        ID = id;
        Count = count;
    }
}
