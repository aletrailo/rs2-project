import { Role } from "./role";
import { JwtPayloadKeys } from "./jwt-payload-keys";

export interface IJwtPayload {
    [JwtPayloadKeys.Username]: string;
    [JwtPayloadKeys.Email]: string;
    [JwtPayloadKeys.Role]: Role | Role[];
    exp: number;
    iss: string;
    aud: string;
}