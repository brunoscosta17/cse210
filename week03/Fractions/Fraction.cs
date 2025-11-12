public class Fraction
{
    private int _top;    // numerador
    private int _bottom; // denominador

    // 1) Construtor sem parâmetros: 1/1
    public Fraction()
    {
        _top = 1;
        _bottom = 1;
    }

    // 2) Construtor com 1 parâmetro (topo): top/1
    public Fraction(int top)
    {
        _top = top;
        _bottom = 1;
    }

    // 3) Construtor com 2 parâmetros (topo e base)
    public Fraction(int top, int bottom)
    {
        // Opcional: validar para evitar divisão por zero
        if (bottom == 0)
        {
            throw new System.ArgumentException("Denominator (bottom) cannot be zero.");
        }

        _top = top;
        _bottom = bottom;
    }

    // Getters
    public int GetTop()
    {
        return _top;
    }

    public int GetBottom()
    {
        return _bottom;
    }

    // Setters
    public void SetTop(int value)
    {
        _top = value;
    }

    public void SetBottom(int value)
    {
        if (value == 0)
        {
            throw new System.ArgumentException("Denominator (bottom) cannot be zero.");
        }
        _bottom = value;
    }

    // Representações
    public string GetFractionString()
    {
        return $"{_top}/{_bottom}";
    }

    public double GetDecimalValue()
    {
        // Converta pelo menos um operando para double
        return (double)_top / _bottom;
    }
}
