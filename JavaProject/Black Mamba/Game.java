import javax.swing.*;
import java.awt.*;
import java.awt.event.KeyEvent;
import java.awt.event.KeyListener;

public class Game extends JFrame implements KeyListener {
    private static final int WIDTH = 400;
    private static final int HEIGHT = 400;
    private static final int CELL_SIZE = 20;
    
    private int squareX = WIDTH / 2;
    private int squareY = HEIGHT / 2;
    private boolean trailEnabled = false;
    
    private Game() {
        setSize(WIDTH, HEIGHT);
        setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
        setVisible(true);
        addKeyListener(this);
    }
    
    @Override
    public void paint(Graphics g) {
        super.paint(g);
        Graphics2D g2d = (Graphics2D) g;
        
        if (trailEnabled) {
            g2d.setColor(Color.BLACK);
            g2d.fillRect(squareX, squareY, CELL_SIZE, CELL_SIZE);
        }
        
        g2d.setColor(Color.RED);
        g2d.drawRect(squareX, squareY, CELL_SIZE, CELL_SIZE);
    }
    
    public void keyPressed(KeyEvent e) {
        int keyCode = e.getKeyCode();
        if (keyCode == KeyEvent.VK_LEFT) {
            squareX -= CELL_SIZE;
        } else if (keyCode == KeyEvent.VK_RIGHT) {
            squareX += CELL_SIZE;
        } else if (keyCode == KeyEvent.VK_UP) {
            squareY -= CELL_SIZE;
        } else if (keyCode == KeyEvent.VK_DOWN) {
            squareY += CELL_SIZE;
        } else if (keyCode == KeyEvent.VK_SPACE) {
            trailEnabled = !trailEnabled;
        }
        
        repaint();
    }
    
    public void keyTyped(KeyEvent e) {}
    public void keyReleased(KeyEvent e) {}
    
    public static void main(String[] args) {
        SwingUtilities.invokeLater(Game::new);
    }
}
