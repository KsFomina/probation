using OfficeOpenXml;
using System.IO;
using System.Collections.Generic;

namespace Ex4
{
    public partial class CombinationsParty : Form
    {
        List<Party> parties = new List<Party>();
        List<List<Party>> combinations = new List<List<Party>>();

        //�������� ������ �� ����� � parties
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

        //��������� ������� �� ������ ��������
        private void UploadButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "D:\\"; // �� ������ ������� ������ ��������� ����������
                openFileDialog.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm"; // ������ ��� ������ Excel
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;
                    UploatFile(filePath);
                }
            }
            comboBoxParties.DataSource = parties;
            comboBoxParties.DisplayMember = "Number"; // �����, ������� ����� ������������ � ComboBox
            comboBoxParties.ValueMember = "Number"; // ��������, ������� ����� �������������� � ComboBox
        }

        private void FindCombinations(Party sampleParty, List<Party> AllParty)
        {
            

            // �������� �� ���� ������� � ���� ����������
            foreach (var party in AllParty)
            {
                // ���������� ������-�������
                if (party.Number == sampleParty.Number)
                    continue;

                // ���������, ������������� �� ������� ������ ��������
                if (sampleParty.MatchesCriteria(party))
                {
                    // ��������� � ����������, ��� ������� ��� ����
                    foreach (var combo in combinations)
                    {
                        // ���������, ��� ��� ������ � ���������� ������������� ���� �����
                        if (combo.All(item => item.MatchesCriteria(party)) && !combo.Contains(party)) 
                        {
                            combo.Add(party);
                        }
                    }

                    // ������� ����� ���������� � ������� ������� � ��������
                    combinations.Add(new List<Party> { sampleParty, party });
                }
            }

            // ������� ������������� � �������������
            combinations = RemoveDuplicate�ombinations(combinations);

        }
        private List<List<Party>> RemoveDuplicate�ombinations(List<List<Party>> combinations)
        {
            // ������� ��������� ������ ���������� � ��������� ��
            for (int i = 0; i < combinations.Count; i++)
            {
                combinations[i] = combinations[i].Distinct().ToList();
                combinations[i].Sort((a, b) => a.Number.CompareTo(b.Number));
            }

            // ������ ��� �������� �������� �������������, ������� ����� �������
            var subcombinationIndexes = new HashSet<int>();

            // ���������� ������ ���������� � ����������
            for (int i = 0; i < combinations.Count; i++)
            {
                for (int j = 0; j < combinations.Count; j++)
                {
                    // �� ���������� ���������� � ����� �����
                    if (i == j) continue;

                    // ���� ���������� i �������� ������������� ���������� j, ��������� � ������ ��� ��������
                    if (!subcombinationIndexes.Contains(i) &&
                        new HashSet<Party>(combinations[j]).IsSupersetOf(combinations[i]))
                    {
                        subcombinationIndexes.Add(i);
                    }
                }

               
            }
            // ������� ������������� � �����
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
                // �������� ������� � ��������� �������� ������
                FindCombinations(foundParty, parties);
            }
            else
            {
                // ��������� ������, ����� ������ � ����� ������� �� �������
                Console.WriteLine("������ � ������� " + numberParty + " �� �������.");
            }

            listBoxParties.Items.Clear();

            // �������� �������� � ListBox
            foreach (var partyGroup in combinations)
            {
                // �������� ������ �� ������� ������ � ������, ����������� ������ "+"
                string displayText = string.Join("+", partyGroup.Select(p => p.Number));

                // �������� ������ � ListBox
                listBoxParties.Items.Add(displayText);
            }
        }
    }
}
