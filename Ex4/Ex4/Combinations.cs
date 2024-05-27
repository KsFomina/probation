using OfficeOpenXml;
using System.IO;
using System.Collections.Generic;

namespace Ex4
{
    public partial class CombinationsParty : Form
    {
        List<Party> parties = new List<Party>();
        List<List<Party>> combinations = new List<List<Party>>();

        //загрузка данных из файла в parties
        public void UploatFile(string filePath)
        {
            parties.Clear();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            FileInfo fileInfo = new FileInfo(filePath);

            using (ExcelPackage package = new ExcelPackage(fileInfo))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];

                int totalRows = worksheet.Dimension.End.Row;
                int totalCols = worksheet.Dimension.Columns;



                for (int i = 2; i <= totalRows; i++)
                {
                    if (worksheet.Cells[i, 1].Value != null)
                    {
                        Party record = new Party
                        {
                            Number = worksheet.Cells[i, 1].Value.ToString(),
                            Parameter1 = Convert.ToDouble(worksheet.Cells[i, 2].Value),
                            Parameter2 = Convert.ToDouble(worksheet.Cells[i, 3].Value),
                            Parameter3 = Convert.ToDouble(worksheet.Cells[i, 4].Value),
                            Parameter4 = Convert.ToDouble(worksheet.Cells[i, 5].Value),
                            Parameter5 = Convert.ToDouble(worksheet.Cells[i, 6].Value),
                            Parameter6 = Convert.ToDouble(worksheet.Cells[i, 7].Value),
                            Parameter7 = Convert.ToDouble(worksheet.Cells[i, 8].Value),
                        };

                        parties.Add(record);
                    }
                }
            }
        }
        public CombinationsParty()
        {
            InitializeComponent();
            
        }

        //обработка нажатия на кнопку загрузки
        private void UploadButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "D:\\"; // Вы можете указать другую начальную директорию
                openFileDialog.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm"; // Фильтр для файлов Excel
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;
                    UploatFile(filePath);
                }
            }
            comboBoxParties.DataSource = parties;
            comboBoxParties.DisplayMember = "Number"; // Текст, который будет отображаться в ComboBox
            comboBoxParties.ValueMember = "Number"; // Значение, которое будет использоваться в ComboBox
        }

        private void FindCombinations(Party sampleParty, List<Party> AllParty)
        {
            

            // Проходим по всем партиям и ищем комбинации
            foreach (var party in AllParty)
            {
                // Пропускаем партию-образец
                if (party.Number == sampleParty.Number)
                    continue;

                // Проверяем, соответствует ли текущая партия условиям
                if (sampleParty.MatchesCriteria(party))
                {
                    // Добавляем в комбинации, где образец уже есть
                    foreach (var combo in combinations)
                    {
                        // Проверяем, что все партии в комбинации соответствуют друг другу
                        if (combo.All(item => item.MatchesCriteria(party)) && !combo.Contains(party)) 
                        {
                            combo.Add(party);
                        }
                    }

                    // Создаем новую комбинацию с текущей партией и образцом
                    combinations.Add(new List<Party> { sampleParty, party });
                }
            }

            // Удаляем повторяющиеся и подкомбинации
            combinations = RemoveDuplicateСombinations(combinations);

        }
        private List<List<Party>> RemoveDuplicateСombinations(List<List<Party>> combinations)
        {
            // Удаляем дубликаты внутри комбинаций и сортируем их
            for (int i = 0; i < combinations.Count; i++)
            {
                combinations[i] = combinations[i].Distinct().ToList();
                combinations[i].Sort((a, b) => a.Number.CompareTo(b.Number));
            }

            // Список для хранения индексов подкомбинаций, которые нужно удалить
            var subcombinationIndexes = new HashSet<int>();

            // Сравниваем каждую комбинацию с остальными
            for (int i = 0; i < combinations.Count; i++)
            {
                for (int j = 0; j < combinations.Count; j++)
                {
                    // Не сравниваем комбинацию с самой собой
                    if (i == j) continue;

                    // Если комбинация i является подмножеством комбинации j, добавляем её индекс для удаления
                    if (!subcombinationIndexes.Contains(i) &&
                        new HashSet<Party>(combinations[j]).IsSupersetOf(combinations[i]))
                    {
                        subcombinationIndexes.Add(i);
                    }
                }

               
            }
            // удаляем подкомбинации с конца
            var combinationsWithoutSubcombinations = combinations
            .Where((_, index) => !subcombinationIndexes.Contains(index))
            .ToList();

            return combinationsWithoutSubcombinations;
        }


        private void StartButton_Click(object sender, EventArgs e)
        {
            string numberParty=comboBoxParties.Text;
            Party foundParty = parties.FirstOrDefault(p => p.Number == numberParty);
            if (foundParty != null)
            {
                // Вызовите функцию с найденным объектом партии
                FindCombinations(foundParty, parties);
            }
            else
            {
                // Обработка случая, когда партия с таким номером не найдена
                Console.WriteLine("Партия с номером " + numberParty + " не найдена.");
            }

            listBoxParties.Items.Clear();

            // Добавьте элементы в ListBox
            foreach (var partyGroup in combinations)
            {
                // Создайте строку из номеров партий в группе, разделенных знаком "+"
                string displayText = string.Join("+", partyGroup.Select(p => p.Number));

                // Добавьте строку в ListBox
                listBoxParties.Items.Add(displayText);
            }
        }
    }
}
