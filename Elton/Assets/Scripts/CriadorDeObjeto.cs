using UnityEngine;

public class CriadorDeObjeto : MonoBehaviour
{
    public GameObject objetoASpawna; // O objeto a ser instanciado
    public Transform pontoDeSpawn;   // O ponto de spawn do novo objeto

    private void Start()
    {
        // Verifica se o objeto ainda não existe na cena
        if (objetoASpawna != null && !ObjetoExisteNaCena())
        {
            // Aguarda 5 segundos e, em seguida, cria o objeto
            Invoke("CriarObjeto", 2f);
        }
    }

    private bool ObjetoExisteNaCena()
    {
        // Verifica se existe pelo menos um objeto com o mesmo nome na cena
        GameObject objExistente = GameObject.Find(objetoASpawna.name);
        return objExistente != null;
    }

    private void CriarObjeto()
    {
        // Instancia o novo objeto no ponto de spawn
        if (objetoASpawna != null && pontoDeSpawn != null)
        {
            Instantiate(objetoASpawna, pontoDeSpawn.position, pontoDeSpawn.rotation);
        }
    }
}