//Enunciado
//Crie uma aplicação Console que contenha uma cópia da classe abaixo.

//    public class Student
//{
//    public string Name { get; set; }
//    public string University { get; set; }
//    public int RollNumber { get; set; }

//    public void DisplayInfo()
//    {
//        Console.WriteLine($"{Name} - {University} - {RollNumber}");
//    }
//}
//1-Em seguida, na classe Program, crie um método chamado DisplayPublicProperties que, usando Reflection,
//exiba todas as Propriedades Públicas da classe Student.
//No método Main da classe Program, coloque uma chamada para o método DisplayPublicProperties.
//2-Agora, adicione na classe Program um outro método chamado CreateInstance que
//- Use Reflection para criar uma instância (objeto) da classe Student e, em seguida;
//- Use Reflection para preencher as propriedades públicas do objeto.
//Não é necessário se preocupar com a adição de novas propriedades, isto é, sempre serão preenchidos apenas o Name,
//University e RollNumber.
//- Use Reflection para chamar o método DisplayInfo do objeto criado no item 2.1.
//3-Ao final do exercício, coloque o seu código em um repositório público no GitHub e submeta o link para avaliação.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Ex_01
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Student> students = new List<Student>();

            Student student = new Student("Felipe", "UFRJ", 3);
            students.Add(student);
            students.Add(new Student("Pedro", "USP", 2));

            CreateInstance(students);

            foreach (var s in students)
            {
                DisplayPublicProperties(s);
            }

        }
        public static void DisplayPublicProperties(Student student)
        {
            PropertyInfo[] propertyInfos = student.GetType().GetProperties();
            Console.WriteLine($"Temos {propertyInfos.Length} propriedades públicas na classe Student:");
            foreach (var propertyInfo in propertyInfos)
            {
                Console.WriteLine($"{propertyInfo.Name}, do tipo '{propertyInfo.PropertyType.Name}', com valor '{propertyInfo.GetValue(student)}'.");

            }
        }
        public static void CreateInstance(List<Student> students)
        {
            //var types = Assembly.GetExecutingAssembly().GetTypes();
            //foreach (var type in types)
            //{
            //    if (type.IsAssignableTo(typeof(Student)) && type.IsClass && !type.IsAbstract)
            //    {
            var strategies = new Dictionary<Type, Func<string,object>>();
            strategies.Add(typeof(int), (string input) => int.Parse(input));
            strategies.Add(typeof(string), (string input) => (input));
            var instance = (Student)Activator.CreateInstance(typeof(Student));
            PropertyInfo[] proInfos = typeof(Student).GetProperties();
            foreach (var propertyInfo in proInfos)
            {
                Console.WriteLine($"{propertyInfo.Name}, do tipo '{propertyInfo.PropertyType.Name}'.");
                Console.Write($"Digite o {propertyInfo.Name}: ");
                var input = Console.ReadLine();
                var value = strategies[propertyInfo.PropertyType](input);
                propertyInfo.SetValue(instance, value, null);
            }
            var name = instance.Name;
            var university = instance.University;
            var number = instance.RollNumber;

            var info = new Student(name, university, number);
            students.Add(info);
            info.DisplayInfo();

            //    }
            //}
        }
    }
}
