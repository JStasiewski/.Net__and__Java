import javax.swing.*;
import java.awt.*;
import javax.imageio.ImageIO;

import java.awt.event.KeyEvent;
import java.awt.event.KeyListener;
import java.awt.image.BufferedImage;
import java.util.Random;
import java.awt.geom.AffineTransform;

public class Xonix extends JPanel implements Runnable, KeyListener {

    boolean isRunning;
    Thread thread;
    BufferedImage win , view, titles, gameOver, enemy; 

    int N = 40 , M = 25;
    int titleSize = 16;
    int WIDTH = titleSize * N;
    int HEIGHT = titleSize * M;
    boolean right, left, up, down;
    int x = 0, y = 0, dx = 0, dy = 0;
    double timer = 0.0, delay = 0.5;
    int[][] grid;
    Xonix.Enemy[] enemies;
    double rotate;

    int enemy_speed = 6;
    int enemyCount = 4;


    public Xonix() {
        setPreferredSize(new Dimension(WIDTH,HEIGHT));
        addKeyListener(this);
    }

    public static void main(String[] args) {
        JFrame w = new JFrame("Xonix");
        w.setResizable(false);
        w.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
        w.add(new Xonix());
        w.pack();
        w.setLocationRelativeTo(null);
        w.setVisible(true);
    }

    @Override
    public void addNotify() {
        super.addNotify();
        if(thread==null) {
            thread = new Thread(this);
            isRunning = true;
            thread.start();
        }
    }

    void drop(int y, int x) {
        if (grid[y][x] == 0)
            grid[y][x] = -1;
        if (grid[y - 1][x] == 0)
            drop(y - 1, x);
        if (grid[y + 1][x] == 0)
            drop(y + 1, x);
        if (grid[y][x - 1] == 0)
            drop(y, x - 1);
        if (grid[y][x + 1] == 0)
            drop(y, x + 1);

    }

    public void start() {
         try {
            view = new  BufferedImage(WIDTH, HEIGHT, BufferedImage.TYPE_INT_RGB);
            titles = ImageIO.read(getClass().getResource("square2.png"));
            gameOver = ImageIO.read(getClass().getResource("game_over.png"));
            win = ImageIO.read(getClass().getResource("win.png"));
            enemy = ImageIO.read(getClass().getResource("zibi.png"));
            //enemy = titles.getSubimage(40, 0, titleSize, titleSize);

            grid = new int[M][N];
            for (int i = 0; i < M; i++) {
                for(int j=0; j < N; j++) {
                    if (i==0 || j==0 || i == M-1 || j == N-1) {
                        grid[i][j] = 1;
                    }
                }
            }
            
            enemies = new Xonix.Enemy[enemyCount];
            for (int i = 0; i < enemyCount; i++) {
                enemies[i] = new Xonix.Enemy();
            }

        }  catch (Exception e) {
                e.printStackTrace();
            }
    }

    public void update() {
    rotate += 16;

    if (rotate >= 360)
        rotate = 0;

    if (left) {
        dx = -1;
        dy = 0;
    }
    if (right) {
        dx = 1;
        dy = 0;
    }
    if (up) {
        dx = 0;
        dy = -1;
    }
    if (down) {
        dx = 0;
        dy = 1;
    }

    timer += 0.3;

    if (timer > delay) {
        x += dx;
        y += dy;

        if (x < 0)
            x = 0;
        if (x > N - 1)
            x = N - 1;
        if (y < 0)
            y = 0;
        if (y > M - 1)
            y = M - 1;

        if (grid[y][x] == 2) {
            isRunning = false;
        }

        if (grid[y][x] == 0)
            grid[y][x] = 2;

        timer = 0;
    }

    for (int i = 0; i < enemyCount; i++) {
        enemies[i].move();
    }

    if (grid[y][x] == 1) {
        dx = dy = 0;

        for (int i = 0; i < enemyCount; i++) {
            drop(enemies[i].posY / titleSize, enemies[i].posX / titleSize);
        }

        for (int i = 0; i < M; i++) {
            for (int j = 0; j < N; j++) {
                if (grid[i][j] == -1)
                    grid[i][j] = 0;
                else
                    grid[i][j] = 1;
            }
        }
    }

    for (int i = 0; i < enemyCount; i++) {
        if (enemies[i].isCollided(2)) {
            isRunning = false;
            break;
        }
    }

    int totalTiles = M * N;
    int filledTiles = 0;
    for (int i = 0; i < M; i++) {
        for (int j = 0; j < N; j++) {
            if (grid[i][j] == 2)
                filledTiles++;
        }
    }

    double filledPercentage = (double) filledTiles / totalTiles;
    if (filledPercentage >= 0.75) {
        isRunning = false;
        System.out.println("Congratulations! You filled 75% of the area.");
    }
}

    public void draw() {
        Graphics2D g2 = (Graphics2D) view.getGraphics();
        g2.setColor(Color.BLACK);
        g2.fillRect(0, 0, WIDTH, HEIGHT);

        for (int i = 0; i < M; i++) {
            for (int j = 0; j < N; j++) {
                BufferedImage tile = null;
                
                if(grid[i][j] == 0)
                    continue;
                
                if(grid[i][j] == 1)
                    tile = titles.getSubimage(20, 0, titleSize, titleSize);
            
                if(grid[i][j] == 2)
                    tile = titles.getSubimage(40, 0, titleSize, titleSize);

                if (tile != null) {
                    g2.drawImage(tile, j*titleSize, i*titleSize, null);
                }
            }
        }

        g2.drawImage (
            titles.getSubimage(0, 0, titleSize, titleSize),
            x * titleSize,
            y * titleSize,
            titleSize,
            titleSize,
            null
        );

        for (int i = 0; i < enemies.length; i++) {
            AffineTransform transform =  AffineTransform.getTranslateInstance(enemies[i].posX, 
                                                                               enemies[i].posY);
            transform.rotate(Math.toRadians(rotate), 10, 10);
            g2.drawImage(enemy, transform, null); 
        }

        if (checkWinCondition()) {
        isRunning = false;
        g2.drawImage(
            win,
            WIDTH / 10 * 3,
            HEIGHT / 10 * 3,
            gameOver.getWidth(),
            gameOver.getHeight(),
            null
        );
    } else if (!isRunning) {
        g2.drawImage(
            gameOver,
            WIDTH / 10 * 3,
            HEIGHT / 10 * 3,
            gameOver.getWidth(),
            gameOver.getHeight(),
            null
        );
    }

        int totalTiles = (M - 2) * (N - 2);
        int filledTiles = 0;
        for (int i = 1; i < M - 1; i++) {
            for (int j = 1; j < N - 1; j++) {
                if (grid[i][j] == 1)
                    filledTiles++;
        }
    }

    double filledPercentage = (double) filledTiles / totalTiles;
    int percentage = (int) (filledPercentage * 100);

    // Draw the percentage filled
    String percentageText = "Filled: " + percentage + "%";
    g2.setColor(Color.WHITE);
    g2.setFont(new Font("Arial", Font.BOLD, 16));
    g2.drawString(percentageText, 10, HEIGHT - 10);

    // Draw the updated view to the panel
    Graphics g = getGraphics();
    g.drawImage(view, 0, 0, WIDTH, HEIGHT, null);
    }

    @Override
    public void run() {
        try {
            requestFocus();
            start();
            while (isRunning) {
                update();
                draw();
                Thread.sleep(1000/60);

            }
        } catch (Exception e) {
            e.printStackTrace();
        }

    }

    @Override
    public void keyTyped(KeyEvent e) {

    }

    @Override
    public void keyPressed(KeyEvent e) {
        if (e.getKeyCode() == KeyEvent.VK_RIGHT) 
            right = true;
        
        if (e.getKeyCode() == KeyEvent.VK_LEFT) 
            left = true;
        
        if (e.getKeyCode() == KeyEvent.VK_UP) 
            up = true;
        
        if (e.getKeyCode() == KeyEvent.VK_DOWN) 
            down = true;
        
        if (e.getKeyCode() == KeyEvent.VK_Y)
            restartGame();
        
        if (e.getKeyCode() == KeyEvent.VK_N)
            System.exit(0);

    }

    @Override
    public void keyReleased(KeyEvent e) {
        if (e.getKeyCode() == KeyEvent.VK_RIGHT) {
            right = false;
        }
        if (e.getKeyCode() == KeyEvent.VK_LEFT) {
            left = false;
        }
        if (e.getKeyCode() == KeyEvent.VK_UP) {
            up = false;
        }
        if (e.getKeyCode() == KeyEvent.VK_DOWN) {
            down = false;
        }
    }


    public class Enemy {
        int posX, posY, dx, dy;
        int size = 40;

        public Enemy() {
            posX = posY = 300;
            int valueDX = (4 - (new Random().nextInt(8)));
            int valueDY = (4 - (new Random().nextInt(8)));
            dx = valueDX == 0 ? 4 : valueDX;
            dy = valueDY == 0 ? 4 : valueDY;
        }

        void move() {
            posX += dx * enemy_speed;
            
            if (isCollided(1)) {
                dx = -dx;
                posX += dx * enemy_speed;
            }

            posY += dy * enemy_speed;

            if(isCollided(1)) {
                dy = -dy;
                posY += dy * enemy_speed;
            }
}

        boolean isCollided(int object) {
            return (grid[posY / titleSize][posX / titleSize] == object ||
                    grid[((posY + size) / titleSize) - 1][posX / titleSize] == object ||
                    grid[posY / titleSize][((posX + size) / titleSize) - 1] == object ||
                    grid[((posY + size) / titleSize) - 1][((posX + size) / titleSize) - 1] == object);
        }
    }

    public void restartGame() {

    x = 0;
    y = 0;
    dx = 0;
    dy = 0;

    for (int i = 0; i < M; i++) {
        for (int j = 0; j < N; j++) {
            if (i == 0 || j == 0 || i == M - 1 || j == N - 1) {
                grid[i][j] = 1;
            } else {
                grid[i][j] = 0;
            }
        }
    }

    for (int i = 0; i < enemyCount; i++) {
        enemies[i] = new Enemy();
    }

    isRunning = true;
    }

    public boolean askToRestart(KeyEvent key) {
    while (true) {
        try {
            Thread.sleep(100);
        } catch (InterruptedException e) {
            e.printStackTrace();
        }
        
        if (key.getKeyCode() == KeyEvent.VK_Y) {
            return true;
        } else if (key.getKeyCode() == KeyEvent.VK_N) {
            return false;
        }
    }
    }

    public boolean checkWinCondition() {
    int totalCells = (M-2)*(N-2);  // Total number of cells in the grid
    int filledCells = 0;    // Count of cells equal to 1
    
    for (int i = 1; i < M-1; i++) {
        for (int j = 1; j < N-1; j++) {
            if (grid[i][j] == 1) {
                filledCells++;
            }
        }
    }
    
    double filledPercentage = (double) filledCells / totalCells * 100;
    
    return filledPercentage >= 75.0;
}
}