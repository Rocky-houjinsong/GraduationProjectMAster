namespace ToRead.Services;

/// <summary>
/// IBrowserService �ӿڵ������Ƿ�װ�˵���ϵͳĬ���������ָ�� URL �Ĺ��ܡ�
/// ͨ����Ӧ�ó���������ע�� IBrowserService �ӿڣ�
/// ������������Ϳ���ʹ�øýӿ��ṩ�ķ�������ָ���� URL��
/// ������Ҫֱ�ӵ���ϵͳ API ��Ӳ�������ʵ���߼����������Դ������õĽ���Ϳɲ�����
/// </summary>
public interface IBrowserService
{
    Task OpenAsync(string url);
}