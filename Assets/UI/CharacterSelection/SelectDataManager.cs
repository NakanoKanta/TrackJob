/// <summary>
///     選んだキャラのデータを保存するクラス
/// </summary>
public static class SelectDataManager
{
    /// <summary>
    ///     選んだキャラのデータ
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
    ///     選んだキャラのデータを保存する
    /// </summary>
    /// <param name="data1Index"></param>
    /// <param name="data2Index"></param>
    public static void SetSelectData(int data1Index, int data2Index)
    {
        _currentData = new SelectData(data1Index, data2Index); 
    }

    /// <summary>
    ///     保存されたデータを破棄する
    /// </summary>
    public  static void DisposeSelectData()
    {
        _currentData = null;
    }
}
