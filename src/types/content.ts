
export interface NavLink {
  key: string;
  href: string;
  submenu?: { key: string; href: string }[];
}