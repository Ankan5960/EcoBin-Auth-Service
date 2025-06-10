namespace EcoBin_Auth_Service.Extensions.Script;

public class MigrationScript
{
    public static string MigrationScriptString { get; } = @"
    DROP TABLE IF EXISTS RefreshTokens CASCADE;
    DROP TABLE IF EXISTS UserRoles CASCADE;
    DROP TABLE IF EXISTS ""User"" CASCADE;
    DROP TABLE IF EXISTS RegistrationKeys CASCADE;
    DROP TABLE IF EXISTS Roles CASCADE;

    CREATE TABLE IF NOT EXISTS Roles(
            role_id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
            role_name VARCHAR(50) UNIQUE NOT NULL
    );

    CREATE TABLE IF NOT EXISTS RegistrationKeys(
        key_id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
        registration_key VARCHAR(255) UNIQUE NOT NULL,  
        role_id UUID NOT NULL,
        is_used BOOLEAN DEFAULT FALSE,                  
        expires_at TIMESTAMP,
        created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
        delete_at TIMESTAMP,
        FOREIGN KEY(role_id) REFERENCES Roles(role_id) ON DELETE CASCADE
    );

    CREATE TABLE IF NOT EXISTS ""User"" (
        user_id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
        user_name VARCHAR(100) UNIQUE NOT NULL,
        email VARCHAR(255) UNIQUE NOT NULL,
        first_name VARCHAR(255) NOT NULL,
        last_name VARCHAR(255),
        password_hash VARCHAR(255) NOT NULL,
        created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
        updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
        deleted_at TIMESTAMP,
        is_verified BOOLEAN DEFAULT false,
        registration_key_id UUID,
        FOREIGN KEY(registration_key_id) REFERENCES RegistrationKeys(key_id) ON DELETE SET NULL
    );

    CREATE TABLE IF NOT EXISTS UserRoles(
        user_id UUID NOT NULL,
        role_id UUID NOT NULL,
        PRIMARY KEY (user_id, role_id),
        FOREIGN KEY (user_id) REFERENCES ""User"" (user_id) ON DELETE CASCADE,
        FOREIGN KEY(role_id) REFERENCES Roles(role_id) ON DELETE CASCADE
    );

    CREATE TABLE IF NOT EXISTS RefreshTokens (
        token_id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
        user_id UUID NOT NULL,
        refresh_token VARCHAR(255) NOT NULL,
        device_info VARCHAR(255),  
        ip_address VARCHAR(50),    
        expires_at TIMESTAMP NOT NULL,
        is_revoked BOOLEAN DEFAULT FALSE,
        created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
        updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
        FOREIGN KEY (user_id) REFERENCES ""User"" (user_id) ON DELETE CASCADE
    );

    INSERT INTO Roles(role_name) VALUES
        ('Admin'),
        ('Collector'),
        ('User'),
        ('Guest')
    ON CONFLICT(role_name) DO NOTHING;
    ";
}
