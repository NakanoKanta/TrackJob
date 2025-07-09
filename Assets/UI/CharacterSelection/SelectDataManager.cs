/// <summary>
///     �I�񂾃L�����̃f�[�^��ۑ�����N���X
/// </summary>
public static class SelectDataManager
{
    /// <summary>
    ///     �I�񂾃L�����̃f�[�^
    /// </summary>
    public class SelectData
    {
        public SelectData(int data1Index, int data2Index)
        {
            Data1Index = data1Index;
            Data2Index = data2Index;
        }

        public readonly int Data1Index;
        public readonly int Data2Index;
    }

    public static SelectData CurrentData => _currentData;

    private static SelectData _currentData = null;

    /// <summary>
    ///     �I�񂾃L�����̃f�[�^��ۑ�����
    /// </summary>
    /// <param name="data1Index"></param>
    /// <param name="data2Index"></param>
    public static void SetSelectData(int data1Index, int data2Index)
    {
        _currentData = new SelectData(data1Index, data2Index); 
    }

    /// <summary>
    ///     �ۑ����ꂽ�f�[�^��j������
    /// </summary>
    public  static void DisposeSelectData()
    {
        _currentData = null;
    }
}
