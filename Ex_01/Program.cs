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

            Student newStudent = Activator.CreateInstance<Student>();

            CreateInstance(student);

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
        public static void CreateInstance(Student student)
        {

        }
    }
}
